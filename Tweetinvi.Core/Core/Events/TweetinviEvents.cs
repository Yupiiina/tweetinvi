﻿using System;
using Tweetinvi.Core.Exceptions;
using Tweetinvi.Events;

namespace Tweetinvi.Core.Events
{
    public interface ITweetinviEvents : ITwitterClientEvents
    {
    }

    public class TweetinviEvents : TwitterClientEvents, ITweetinviEvents
    {
    }

    public interface IExternalClientEvents
    {
        /// <summary>
        /// This is the first event raised. This event is raised before executing a request when the rate limits for the query have been retrieved.
        /// At that stage you have the information regarding how many requests can be performed and how long you have to wait if no more request are available.
        /// This event will let you log, modify, cancel a request.
        /// Use this event if you wish to manually handle rate limits
        /// </summary>
        event EventHandler<BeforeExecutingRequestEventArgs> BeforeWaitingForRequestRateLimits;

        /// <summary>
        /// Event raised before executing a request. At that stage we have waited for rate limits to be available.
        /// The request will be executed right after this event.
        /// </summary>
        event EventHandler<BeforeExecutingRequestEventArgs> BeforeExecutingRequest;

        /// <summary>
        /// Event raised after a request has been performed, this event will let you log the query and check the result/exception.
        /// </summary>
        event EventHandler<AfterExecutingQueryEventArgs> AfterExecutingRequest;

        /// <summary>
        /// Event raised when an exception is returned by the TwitterApi service
        /// </summary>
        event EventHandler<ITwitterException> OnTwitterException;
    }

    public interface ITwitterClientEvents : IExternalClientEvents
    {
        void RaiseBeforeWaitingForQueryRateLimits(BeforeExecutingRequestEventArgs beforeExecutingRequestExecutedEventArgs);
        void RaiseBeforeExecutingQuery(BeforeExecutingRequestEventArgs beforeExecutingRequestExecutedEventArgs);
        void RaiseAfterExecutingQuery(AfterExecutingQueryEventArgs afterExecutingQueryEventArgs);
        void RaiseOnTwitterException(ITwitterException exception);
    }

    public class TwitterClientEvents : ITwitterClientEvents
    {
        public event EventHandler<BeforeExecutingRequestEventArgs> BeforeWaitingForRequestRateLimits;
        public void RaiseBeforeWaitingForQueryRateLimits(BeforeExecutingRequestEventArgs beforeExecutingRequestAfterExecuteEventArgs)
        {
            this.Raise(BeforeWaitingForRequestRateLimits, beforeExecutingRequestAfterExecuteEventArgs);
        }

        public event EventHandler<BeforeExecutingRequestEventArgs> BeforeExecutingRequest;
        public void RaiseBeforeExecutingQuery(BeforeExecutingRequestEventArgs beforeExecutingRequestExecutedEventArgs)
        {
            this.Raise(BeforeExecutingRequest, beforeExecutingRequestExecutedEventArgs);
        }

        public event EventHandler<AfterExecutingQueryEventArgs> AfterExecutingRequest;
        public void RaiseAfterExecutingQuery(AfterExecutingQueryEventArgs afterExecutingQueryEventArgs)
        {
            this.Raise(AfterExecutingRequest, afterExecutingQueryEventArgs);
        }

        public event EventHandler<ITwitterException> OnTwitterException;
        public void RaiseOnTwitterException(ITwitterException exception)
        {
            this.Raise(OnTwitterException, exception);
        }
    }
}