using System.Threading.Tasks;
using Tweetinvi.Core.Iterators;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.DTO.QueryDTO;
using Tweetinvi.Parameters;

namespace Tweetinvi.Client.Requesters
{
    /// <summary>
    /// A client providing all the actions relative to an account and its authenticated user.
    /// The results from this client contain additional metadata.
    /// </summary>
    public interface IAccountRequester
    {
        /// <summary>
        /// Get the authenticated user based on the TwitterClient's credentials
        /// <para>Read more : https://dev.twitter.com/rest/reference/get/account/verify_credentials </para>
        /// </summary>
        /// <returns>TwitterResult containing the client's authenticated user</returns>
        Task<ITwitterResult<IUserDTO, IAuthenticatedUser>> GetAuthenticatedUser(IGetAuthenticatedUserParameters parameters);

        /// <summary>
        /// Block a user from the client's account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/post-blocks-create </para>
        /// </summary>
        /// <returns>TwitterResult containing the blocked user</returns>
        Task<ITwitterResult<IUserDTO>> BlockUser(IBlockUserParameters parameters);

        /// <summary>
        /// Unblock a user from the client's account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/post-blocks-destroy </para>
        /// </summary>
        /// <returns>TwitterResult containing the unblocked user</returns>
        Task<ITwitterResult<IUserDTO>> UnblockUser(IUnblockUserParameters parameters);

        /// <summary>
        /// Report a user for spam
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/post-users-report_spam </para>
        /// </summary>
        /// <returns>TwitterResult containing the reported user</returns>
        Task<ITwitterResult<IUserDTO>> ReportUserForSpam(IReportUserForSpamParameters parameters);

        /// <summary>
        /// Get the user ids blocked by the client's account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/get-blocks-ids </para>
        /// </summary>
        /// <returns>An iterator to list the blocked user ids</returns>
        ITwitterPageIterator<ITwitterResult<IIdsCursorQueryResultDTO>> GetBlockedUserIdsIterator(IGetBlockedUserIdsParameters parameters);

        /// <summary>
        /// Get the users blocked by the client's account
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/get-blocks-ids </para>
        /// </summary>
        /// <returns>An iterator to list the blocked users</returns>
        ITwitterPageIterator<ITwitterResult<IUserCursorQueryResultDTO>> GetBlockedUsersIterator(IGetBlockedUsersParameters parameters);

        // FOLLOWERS

        /// <summary>
        /// Follow a user
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/post-friendships-create </para>
        /// </summary>
        /// <returns>TwitterResult containing the followed user</returns>
        Task<ITwitterResult<IUserDTO>> FollowUser(IFollowUserParameters parameters);

        /// <summary>
        /// Modify the relationship between the authenticated user (source) and another user (target).
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/post-friendships-update </para>
        /// </summary>
        /// <returns>TwitterResult containing the updated relationship details</returns>
        Task<ITwitterResult<IRelationshipDetailsDTO, IRelationshipDetails>> UpdateRelationship(IUpdateRelationshipParameters parameters);
        
        /// <summary>
        /// Stops following a user
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/post-friendships-destroy </para>
        /// </summary>
        /// <returns>TwitterResult containing the user who is no longer followed</returns>
        Task<ITwitterResult<IUserDTO>> UnFollowUser(IUnFollowUserParameters parameters);

        // ONGOING REQUESTS

        /// <summary>
        /// Get the pending follower requests you have received.
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-friendships-incoming </para>
        /// <para>Protected accounts : https://help.twitter.com/en/safety-and-security/public-and-protected-tweets</para>
        /// </summary>
        /// <returns>An iterator to list the users who requested to follow the client's account</returns>
        ITwitterPageIterator<ITwitterResult<IIdsCursorQueryResultDTO>> GetUserIdsRequestingFriendshipIterator(IGetUserIdsRequestingFriendshipParameters parameters);

        /// <summary>
        /// Get the pending follower requests that you have requested.
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-friendships-outgoing </para>
        /// <para>Protected accounts : https://help.twitter.com/en/safety-and-security/public-and-protected-tweets</para>
        /// </summary>
        /// <returns>An iterator to list the user ids the client's account requested to follow</returns>
        ITwitterPageIterator<ITwitterResult<IIdsCursorQueryResultDTO>> GetUserIdsYouRequestedToFollowIterator(IGetUserIdsYouRequestedToFollowParameters parameters);

        // FRIENDSHIPS

        /// <summary>
        /// Get the relationships of the client's user with multiple other users
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-friendships-lookup </para>
        /// </summary>
        /// <returns>TwitterResult containing the relationships between the authenticated user and multiple other users</returns>
        Task<ITwitterResult<IRelationshipStateDTO[], IRelationshipState[]>> GetRelationshipsWith(IGetRelationshipsWithParameters parameters);

        /// <summary>
        /// Get the user ids for whom the retweets are muted
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-friendships-no_retweets-ids </para>
        /// </summary>
        /// <returns>TwitterResult containing a list of user ids for whom the retweets are muted</returns>
        Task<ITwitterResult<long[]>> GetUserIdsWhoseRetweetsAreMuted(IGetUserIdsWhoseRetweetsAreMutedParameters parameters);

        /// <summary>
        /// Get the muted user ids.
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/get-mutes-users-ids </para>
        /// </summary>
        /// <returns>An iterator to list the user ids muted by the client's account</returns>
        ITwitterPageIterator<ITwitterResult<IIdsCursorQueryResultDTO>> GetMutedUserIdsIterator(IGetMutedUserIdsParameters parameters);
        
        /// <summary>
        /// Get the muted user ids.
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/get-mutes-users-list </para>
        /// </summary>
        /// <returns>An iterator to list the users muted by the client's account</returns>
        ITwitterPageIterator<ITwitterResult<IUserCursorQueryResultDTO>> GetMutedUsersIterator(IGetMutedUsersParameters parameters);

        /// <summary>
        /// Mute a user
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/post-mutes-users-create </para>
        /// </summary>
        /// <returns>Twitter result containing the authenticated user</returns>
        Task<ITwitterResult<IUserDTO>> MuteUser(IMuteUserParameters parameters);
        
        /// <summary>
        /// Remove the mute of a user
        /// <para>Read more : https://dev.twitter.com/en/docs/accounts-and-users/mute-block-report-users/api-reference/post-mutes-users-destroy </para>
        /// </summary>
        /// <returns>Twitter result containing the authenticated user</returns>
        Task<ITwitterResult<IUserDTO>> UnMuteUser(IUnMuteUserParameters parameters);
    }
}