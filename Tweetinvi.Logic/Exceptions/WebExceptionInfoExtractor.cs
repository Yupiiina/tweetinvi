﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Tweetinvi.Core.Exceptions;
using Tweetinvi.Core.Injectinvi;
using Tweetinvi.Core.Wrappers;
using Tweetinvi.Logic.Properties;

namespace Tweetinvi.Logic.Exceptions
{
    public class WebExceptionInfoExtractor : IWebExceptionInfoExtractor
    {
        private readonly IJObjectStaticWrapper _jObjectStaticWrapper;
        private readonly IFactory<ITwitterExceptionInfo> _twitterExceptionInfoUnityFactory;

        public WebExceptionInfoExtractor(
            IJObjectStaticWrapper jObjectStaticWrapper,
            IFactory<ITwitterExceptionInfo> twitterExceptionInfoUnityFactory)
        {
            _jObjectStaticWrapper = jObjectStaticWrapper;
            _twitterExceptionInfoUnityFactory = twitterExceptionInfoUnityFactory;
        }

        public int GetWebExceptionStatusNumber(WebException wex)
        {
            return GetWebExceptionStatusNumber(wex, -1);
        }

        public int GetWebExceptionStatusNumber(WebException wex, int defaultStatusCode)
        {
            if (wex.Response is HttpWebResponse wexResponse)
            {
                return (int)wexResponse.StatusCode;
            }

            return defaultStatusCode;
        }

        public string GetStatusCodeDescription(int statusCode)
        {
            return Resources.GetResourceByName($"ExceptionDescription_{statusCode}");
        }

        public IEnumerable<ITwitterExceptionInfo> GetTwitterExceptionInfo(WebException wex)
        {
            var wexResponse = wex.Response as HttpWebResponse;

            if (wexResponse == null)
            {
                return Enumerable.Empty<ITwitterExceptionInfo>();
            }

            try
            {
                return GetStreamInfo(wexResponse);
            }
            catch (WebException) { }

            return Enumerable.Empty<ITwitterExceptionInfo>();
        }

        private IEnumerable<ITwitterExceptionInfo> GetStreamInfo(HttpWebResponse wexResponse)
        {
            using (var stream = wexResponse.GetResponseStream())
            {
                return GetTwitterExceptionInfosFromStream(stream);
            }
        }

        public IEnumerable<ITwitterExceptionInfo> GetTwitterExceptionInfosFromStream(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            string twitterExceptionInfo = null;
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    twitterExceptionInfo = reader.ReadToEnd();
                    var jObject = _jObjectStaticWrapper.GetJobjectFromJson(twitterExceptionInfo);
                    return _jObjectStaticWrapper.ToObject<IEnumerable<ITwitterExceptionInfo>>(jObject["errors"]);
                }
            }
            catch (Exception)
            {
                var twitterInfo = _twitterExceptionInfoUnityFactory.Create();
                twitterInfo.Message = twitterExceptionInfo;
                return new[] {twitterInfo};
            }
        }
    }
}