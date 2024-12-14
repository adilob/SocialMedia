USE [SocialMediaDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFollowedProfilesPosts]
    @ProfileId UNIQUEIDENTIFIER,
    @PageNumber INT,
    @RowsPerPage INT
AS
BEGIN
    SET NOCOUNT ON;

    WITH FollowingProfiles AS (
        SELECT 
            IdFollowed
        FROM 
            Connections
        WHERE 
            IdFollower = @ProfileId
    )

    SELECT 
        Posts.Id,
        Posts.Content,
        Posts.PostDate,
        Posts.Likes,
        Posts.ProfileId,
        Posts.CreatedAt,
        Posts.UpdatedAt
    FROM 
        Posts
    INNER JOIN 
        FollowingProfiles ON Posts.ProfileId = FollowingProfiles.IdFollowed
    ORDER BY 
        Posts.PostDate DESC, 
        Posts.Likes DESC
    OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
    FETCH NEXT @RowsPerPage ROWS ONLY;
END
GO
