USE [FTSS]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](500) NOT NULL,
	[Password] [varbinary](200) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[DelFlag] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateToken] [varchar](36) NOT NULL,
	[ChangeDate] [datetime] NULL,
	[ChangeToken] [varchar](36) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_Users]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_Users]
AS
SELECT  UserId, Email, Password, FirstName, LastName
FROM     dbo.Users WITH (nolock)
WHERE   (DelFlag = 0)
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[Token] [varchar](36) NOT NULL,
	[UserId] [int] NOT NULL,
	[GenerateDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_UserTokens]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_UserTokens]
AS
SELECT  Token, UserId, GenerateDate, ExpireDate
FROM     dbo.UserTokens WITH (nolock)
WHERE   (ExpireDate >= GETDATE())
GO
/****** Object:  Table [dbo].[UserAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccessMenu](
	[UserAccessMenuId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[DelFlag] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateToken] [varchar](36) NOT NULL,
	[ChangeDate] [datetime] NULL,
	[ChangeToken] [varchar](36) NULL,
 CONSTRAINT [PK_UserAccessMenu] PRIMARY KEY CLUSTERED 
(
	[UserAccessMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_UserAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_UserAccessMenu]
AS
SELECT  UserAccessMenuId, UserId, MenuId
FROM     dbo.UserAccessMenu WITH (nolock)
WHERE   (DelFlag = 0)
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] NOT NULL,
	[MenuId_Parent] [int] NULL,
	[MenuTitle] [nvarchar](200) NOT NULL,
	[MenuAddress] [nvarchar](500) NULL,
	[Kind] [tinyint] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_Menu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_Menu]
AS
SELECT  MenuId, MenuId_Parent, MenuTitle, MenuAddress, Kind
FROM     dbo.Menu WITH (nolock)
GO
/****** Object:  View [dbo].[vw_UserAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_UserAccessMenu]
AS
SELECT  A.UserAccessMenuId, A.UserId, M.MenuId, M.MenuId_Parent, M.MenuTitle, 
			M.MenuAddress, M.Kind
FROM     dbo.v_UserAccessMenu AS A INNER JOIN
               dbo.v_Menu AS M ON A.MenuId = M.MenuId
GO
/****** Object:  Table [dbo].[RoleAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAccessMenu](
	[RoleAccessMenuId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[DelFlag] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateToken] [varchar](36) NOT NULL,
	[ChangeDate] [datetime] NULL,
	[ChangeToken] [varchar](36) NULL,
 CONSTRAINT [PK_RoleAccessMenu] PRIMARY KEY CLUSTERED 
(
	[RoleAccessMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_RoleAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_RoleAccessMenu]
AS
SELECT  RoleAccessMenuId, RoleId, MenuId
FROM     dbo.RoleAccessMenu WITH (nolock)
WHERE   (DelFlag = 0)
GO
/****** Object:  View [dbo].[vw_RoleAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_RoleAccessMenu]
AS
SELECT  a.RoleId, m.MenuId, m.MenuId_Parent, m.MenuTitle, m.MenuAddress, m.Kind
FROM     dbo.v_RoleAccessMenu AS a INNER JOIN
               dbo.v_Menu AS m ON a.MenuId = m.MenuId
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[DelFlag] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreateToken] [varchar](36) NOT NULL,
	[ChangeDate] [datetime] NULL,
	[ChangeToken] [varchar](36) NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_UserRoles]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_UserRoles]
AS
SELECT  UserRoleId, UserId, RoleId
FROM     dbo.UserRoles WITH (nolock)
WHERE   (DelFlag = 0)
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
	[RoleTitle] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_Roles]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_Roles]
AS
SELECT  RoleId, RoleTitle
FROM     dbo.Roles WITH (nolock)
GO
/****** Object:  Table [dbo].[APILog]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APILog](
	[APILogId] [int] IDENTITY(1,1) NOT NULL,
	[APIAddress] [varchar](1000) NULL,
	[UserToken] [varchar](36) NULL,
	[Params] [nvarchar](max) NULL,
	[Results] [nvarchar](max) NULL,
	[ErrorMessage] [nvarchar](1000) NULL,
	[StatusCode] [int] NULL,
	[TrackDate] [datetime] NOT NULL,
 CONSTRAINT [PK_APILog] PRIMARY KEY CLUSTERED 
(
	[APILogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[IPAddress] [varchar](20) NULL,
	[MSG] [nvarchar](4000) NOT NULL,
	[Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (1, NULL, N'Users', N'#/Users', 1)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (2, 1, N'Search users', N'/api/Users/GetAll', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (3, 1, N'Add new user', N'/api/Users/Insert', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (4, 1, N'Update a user', N'/api/Users/Update', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (5, 1, N'Delete a user', N'/api/Users/Delete', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (6, 1, N'Change password by admin', N'/api/Users/SetPassword', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (7, 1, N'Change password by own user', N'/api/Users/ChangePassword', 2)
GO
INSERT [dbo].[Menu] ([MenuId], [MenuId_Parent], [MenuTitle], [MenuAddress], [Kind]) VALUES (8, 1, N'Change info by own user', N'/api/Users/UpdateProfile', 2)
GO
SET IDENTITY_INSERT [dbo].[RoleAccessMenu] ON 
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (1, 100, 1, 0, CAST(N'2021-01-27T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (2, 100, 2, 0, CAST(N'2021-01-27T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (3, 100, 3, 0, CAST(N'2021-01-27T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (4, 100, 4, 0, CAST(N'2021-01-27T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (5, 100, 5, 0, CAST(N'2021-01-27T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (6, 100, 6, 0, CAST(N'2021-01-28T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (7, 200, 7, 0, CAST(N'2021-01-28T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[RoleAccessMenu] ([RoleAccessMenuId], [RoleId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (8, 200, 8, 0, CAST(N'2021-01-28T00:00:00.000' AS DateTime), N'SQL', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[RoleAccessMenu] OFF
GO
INSERT [dbo].[Roles] ([RoleId], [RoleTitle]) VALUES (100, N'Admin')
GO
INSERT [dbo].[Roles] ([RoleId], [RoleTitle]) VALUES (200, N'Employee')
GO
SET IDENTITY_INSERT [dbo].[UserAccessMenu] ON 
GO
INSERT [dbo].[UserAccessMenu] ([UserAccessMenuId], [UserId], [MenuId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (1, 1, 1, 0, CAST(N'2021-01-21T15:49:31.450' AS DateTime), N'SQL', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserAccessMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([UserRoleId], [UserId], [RoleId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (1, 1, 100, 0, CAST(N'2021-01-23T13:54:15.057' AS DateTime), N'SQL', NULL, NULL)
GO
INSERT [dbo].[UserRoles] ([UserRoleId], [UserId], [RoleId], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (2, 4, 200, 0, CAST(N'2021-01-23T13:54:15.057' AS DateTime), N'SQL', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Email], [Password], [FirstName], [LastName], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (1, N'admin', 0x356A192B7913B04C54574D18C28D46E6395428AB, N'Saeed', N'Heidarbozorg', 0, CAST(N'2021-01-10T16:25:32.680' AS DateTime), N'SQL', CAST(N'2021-01-28T17:01:54.353' AS DateTime), N'24E66B74-819B-47BF-AE90-DB54E9757EC0')
GO
INSERT [dbo].[Users] ([UserId], [Email], [Password], [FirstName], [LastName], [DelFlag], [CreateDate], [CreateToken], [ChangeDate], [ChangeToken]) VALUES (4, N'heidarbozorg@gmail.com', 0x356A192B7913B04C54574D18C28D46E6395428AB, N'Saeed', N'Heidarbozorg', 0, CAST(N'2021-01-23T11:12:53.987' AS DateTime), N'1', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Log] ADD  CONSTRAINT [DF_Log_Tarikh]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Kind]  DEFAULT ((1)) FOR [Kind]
GO
ALTER TABLE [dbo].[RoleAccessMenu] ADD  CONSTRAINT [DF_RoleAccessMenu_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
ALTER TABLE [dbo].[UserAccessMenu] ADD  CONSTRAINT [DF_UserAccessMenu_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
ALTER TABLE [dbo].[UserAccessMenu] ADD  CONSTRAINT [DF_UserAccessMenu_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_DelFlag]  DEFAULT ((0)) FOR [DelFlag]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Menu] FOREIGN KEY([MenuId_Parent])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Menu]
GO
ALTER TABLE [dbo].[UserAccessMenu]  WITH CHECK ADD  CONSTRAINT [FK_UserAccessMenu_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[UserAccessMenu] CHECK CONSTRAINT [FK_UserAccessMenu_Menu]
GO
ALTER TABLE [dbo].[UserAccessMenu]  WITH CHECK ADD  CONSTRAINT [FK_UserAccessMenu_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserAccessMenu] CHECK CONSTRAINT [FK_UserAccessMenu_Users]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[SP_APILog_Insert]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-02-07
-- Description: Insert API log
-- =============================================  
CREATE PROCEDURE [dbo].[SP_APILog_Insert]  
	@APIAddress varchar(1000),
	@UserToken varchar(36),
	@Params nvarchar(MAX),
	@Results nvarchar(MAX),
	@ErrorMessage nvarchar(1000),
	@StatusCode int
AS  
BEGIN  
 SET NOCOUNT ON;  
  
	 declare @APILogId int  
  
	 insert into dbo.APILog
	 (  
		APIAddress,  
		UserToken,
		Params,
		Results,
		ErrorMessage,
		StatusCode,
		TrackDate
	 )  
	 values  
	 (  
		@APIAddress,
		@UserToken,
		@Params,
		@Results,
		@ErrorMessage,
		@StatusCode,
		getdate()  
	 )  
	 set @APILogId = SCOPE_IDENTITY()  
  
	select @APILogId as Id
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_Log_Insert]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-08
-- Description: Insert new log on dbo.Log
-- =============================================  
CREATE PROCEDURE [dbo].[SP_Log_Insert]  
	@IPAddress varchar(20),  
	@MSG nvarchar(4000)
AS  
BEGIN  
 SET NOCOUNT ON;  
  
	 declare @LogId int  
  
	 insert into dbo.[Log]  
	 (  
		  IPAddress,    
		  MSG,  
		  [Date]   
	 )  
	 values  
	 (  
		  @IPAddress,     
		  @MSG,  
		  getdate()  
	 )  
	 set @LogId = SCOPE_IDENTITY()  
  
-- select @LogId as LogId  
END  
GO
/****** Object:  StoredProcedure [dbo].[SP_Login]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Saeed Heidarbozorrg
-- Create date: 2021-01-10
-- Description:	Check username and password
--				Generate token
-- =============================================
CREATE PROCEDURE [dbo].[SP_Login]
	@Email varchar(200),
	@Password varchar(50),

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	declare
		@BinaryPassword varbinary(200),
		@TokenLifeHour int = 24,
		@UserId int,
		@Token varchar(36),
		@FirstName varchar(50),
		@LastName varchar(50),
		@RoleTitle varchar(50),

		@Now datetime,
		@ExpireDate datetime


	set @BinaryPassword = HashBytes('SHA1', @Password)

	select top 1
		@UserId = UserId,
		@FirstName = FirstName,
		@LastName = LastName
	from
		dbo.v_Users
	where
		Email = @Email
		and [Password] = @BinaryPassword

	if @UserId is null
	begin
		--Not found
		set @ErrorCode = 404
		set @ErrorMessage = 'Username or password is not valid.'
		return
	end

	set @RoleTitle = (
		select top 1 RoleTitle
		from
			dbo.v_UserRoles ur inner join dbo.v_Roles r on ur.RoleId = r.RoleId
		where
			ur.UserId = @UserId
	)

	set @Now = GetDate()			--Get current date time
	set @Token = NEWID()			--Generate new token
	
	--Set token expire date
	set @ExpireDate = dateadd(hour, @TokenLifeHour, @Now)

	begin try
		begin tran
			insert into UserTokens
			(
				Token,
				UserId,
				GenerateDate,
				[ExpireDate]
			)
			values
			(
				@Token,
				@UserId,
				@Now,
				@ExpireDate
			)
		commit
		
		set @ErrorCode = 200
		set @ErrorMessage = ''
		select
			@Email as Email,
			@UserId as UserId,
			@FirstName as FirstName,
			@LastName as LastName,
			@Token as Token, 
			@ExpireDate as [ExpireDate],
			@RoleTitle as RoleTitle
	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_AccessToAPI]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-02-06
-- Description:	Is this user access to a certain API?
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_AccessToAPI]
	@Token varchar(36),				--Requester Token

	@APIAddress nvarchar(500),		--API address to check

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare 
		@UserIdActor int,
		@Result bit = 0

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return	


	if exists
	(
		select
			MenuId 
		from 
			vw_UserAccessMenu
		where 
			UserId = @UserIdActor
			and Kind = 2

		union

		select
			MenuId
		from 
			vw_RoleAccessMenu
		where 
			RoleId in (select RoleId from v_UserRoles where UserId = @UserIdActor)	
			and Kind = 2
	)
		set @Result = 1

	select @Result as Result
	

	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_ChangePassword]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-27
-- Description:	Change password by own user
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_ChangePassword]
	@Token varchar(36),				--Requester Token

	@OldPassword varchar(50),			--Old password
	@NewPassword varchar(50),			--New password

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare 
		@BinaryNewPassword varbinary(200),
		@BinaryOldPassword varbinary(200),
		@UserIdActor int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return	

	set @BinaryNewPassword = HashBytes('SHA1', @NewPassword)
	set @BinaryOldPassword = HashBytes('SHA1', @OldPassword)

	if exists(select top 1 UserId from Users where UserId = @UserIdActor)
	begin
		set @ErrorMessage = 'Record not found.'
		set @ErrorCode = 400
		return
	end

	if exists(select top 1 UserId from Users where UserId = @UserIdActor and [Password] = @BinaryOldPassword)
	begin
		set @ErrorMessage = 'Incorrect password.'
		set @ErrorCode = 400
		return
	end


	begin try
		begin tran
			update [dbo].[Users]
			set
				[Password] = @BinaryNewPassword,
				ChangeDate = GetDate(),
				ChangeToken = @Token
			where
				UserId = @UserIdActor
		
		commit
		select @UserIdActor as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		select 0 as Id
	end catch
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_Delete]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-27
-- Description:	Delete a user
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_Delete]
	@Token varchar(36),				--Requester Token

	@UserId int,					--UserId to be deleted

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare 
		@BinaryPassword varbinary(200),
		@UserIdActor int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return

	if not exists(select top 1 UserId from v_Users where UserId = @UserId)
	begin
		set @ErrorMessage = 'Record not found'
		set @ErrorCode = 404
		return
	end

	begin try
		begin tran
			update [dbo].[Users]
			set
				DelFlag = 1,
				ChangeDate = GetDate(),
				ChangeToken = @Token
			where
				UserId = @UserId
		
		commit
		select @UserId as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		select 0 as Id
	end catch
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_GetAccessMenu]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2020-01-21
-- Description:	Get user access menus
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_GetAccessMenu]
	@Token varchar(36),

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare
		@UserId int

	exec dbo.SP_UserToken_Validation @Token, @UserId out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return
	
	select
		MenuId, 
		MenuId_Parent, 
		MenuTitle,
		MenuAddress,
		Kind
	from 
		vw_UserAccessMenu
	where 
		UserId = @UserId

	union

	select
		MenuId, 
		MenuId_Parent, 
		MenuTitle,
		MenuAddress,
		Kind
	from 
		vw_RoleAccessMenu
	where 
		RoleId in (select RoleId from v_UserRoles where UserId = @UserId)


END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_Insert]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-23
-- Description:	Add new user
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_Insert]
	@Token varchar(36),				--Requester Token

	@Email varchar(500),
	@Password varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare 
		@BinaryPassword varbinary(200),
		@UserIdActor int,
		@NewUserId int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return

	if exists(select top 1 Email from v_Users where Email = @Email)
	begin
		set @ErrorMessage = 'Email address is already exists.'
		set @ErrorCode = 400
		return
	end

	set @BinaryPassword = HashBytes('SHA1', @Password)

	begin try
		begin tran
			insert into [dbo].[Users]
			(
				Email,
				[Password],
				FirstName,
				LastName,
				DelFlag,
				CreateDate,
				CreateToken				
			)
			values
			(
				@Email,
				@BinaryPassword,
				@FirstName,
				@LastName,
				0,
				GETDATE(),
				@Token				
			)
			set @NewUserId = SCOPE_IDENTITY()

			insert into UserRoles
			(
				UserId,
				RoleId,
				DelFlag,
				CreateDate,
				CreateToken
			)
			values
			(
				@NewUserId,
				200,
				0,
				GetDate(),
				@Token
			)
		
		commit
		select @NewUserId as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		
		select 0 as Id
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_SetPassword]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-27
-- Description:	Change password by admin
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_SetPassword]
	@Token varchar(36),				--Requester Token

	@UserId int,					--UserId to be reset password
	@Password varchar(50),			--New password

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare 
		@BinaryPassword varbinary(200),
		@UserIdActor int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return	

	if exists(select top 1 UserId from v_Users where UserId = @UserId)
	begin
		set @ErrorMessage = 'Record not found.'
		set @ErrorCode = 400
		return
	end

	set @BinaryPassword = HashBytes('SHA1', @Password)

	begin try
		begin tran
			update [dbo].[Users]
			set
				[Password] = @BinaryPassword,
				ChangeDate = GetDate(),
				ChangeToken = @Token
			where
				UserId = @UserId
		
		commit
		select @UserId as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		select 0 as Id
	end catch
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_Update]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-27
-- Description:	Edit a user info
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_Update]
	@Token varchar(36),				--Requester Token

	@UserId int,					--UserId to be updated
	@Email varchar(500),			--New email address
	@FirstName varchar(50),			--New first name
	@LastName varchar(50),			--New last name

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare
		@UserIdActor int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return

	if exists(select top 1 UserId from v_Users where Email = @Email and UserId <> @UserId)
	begin
		set @ErrorMessage = 'Email address is already exists.'
		set @ErrorCode = 400
		return
	end

	begin try
		begin tran
			update [dbo].[Users]
			set
				Email = @Email,
				FirstName = @FirstName,
				LastName = @LastName,
				ChangeDate = GetDate(),
				ChangeToken = @Token
			where
				UserId = @UserId
		
		commit
		select @UserId as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		select 0 as Id
	end catch
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_User_UpdateProfile]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-27
-- Description:	Edit profile user info by itself
-- =============================================
CREATE PROCEDURE [dbo].[SP_User_UpdateProfile]
	@Token varchar(36),				--Requester Token

	@FirstName varchar(50),			--New first name
	@LastName varchar(50),			--New last name

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare
		@UserIdActor int

	exec dbo.SP_UserToken_Validation @Token, @UserIdActor out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return

	begin try
		begin tran
			update [dbo].[Users]
			set
				FirstName = @FirstName,
				LastName = @LastName,
				ChangeDate = GetDate(),
				ChangeToken = @Token
			where
				UserId = @UserIdActor
		
		commit
		select @UserIdActor as Id

	end try
	begin catch
		declare @ErrMessage varchar(max)
		set @ErrMessage = ERROR_MESSAGE()

		if @@TRANCOUNT <> 0
			RollBack

		set @ErrMessage = OBJECT_SCHEMA_NAME(@@PROCID) + '.' + OBJECT_NAME(@@PROCID) + ' : ' + @ErrMessage
		exec [dbo].[SP_Log_Insert] 'SQL', @ErrMessage 
		
		set @ErrMessage = @ErrMessage
		set @ErrorCode = 500
		select 0 as Id
	end catch
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Users_GetAll]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-22
-- Description:	Search between users by filter parameters and Pagination
-- =============================================
CREATE PROCEDURE [dbo].[SP_Users_GetAll]
	@Token varchar(36),				--Requester Token

	@Email varchar(500),
	@FirstName varchar(50),
	@LastName varchar(50),

	@StartIndex int,
	@PageSize int,
	@ActualSize int out,			--Record count with these search params

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare
		@UserId int

	exec dbo.SP_UserToken_Validation @Token, @UserId out, @ErrorCode out, @ErrorMessage out
	if @ErrorCode <> 200 or @ErrorMessage <> ''
		return

	set @ActualSize =
	(
		select count(UserId) 
		from
			dbo.v_Users
		where
			(isnull(@Email, '') = '' or Email like '%' + @Email + '%')
			and (isnull(@FirstName, '') = '' or FirstName like '%' + @FirstName + '%')
			and (isnull(@LastName, '') = '' or LastName like '%' + @LastName + '%')
	)

	select
		UserId,
		Email,
		FirstName,
		LastName
	from
		dbo.v_Users
	where
		(isnull(@Email, '') = '' or Email like '%' + @Email + '%')
		and (isnull(@FirstName, '') = '' or FirstName like '%' + @FirstName + '%')
		and (isnull(@LastName, '') = '' or LastName like '%' + @LastName + '%')
	order by UserId desc
		OFFSET @StartIndex ROWS  
	FETCH NEXT @PageSize ROWS ONLY  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UserToken_Validation]    Script Date: 3/2/2021 8:49:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Saeed Heidarbozorg
-- Create date: 2021-01-21
-- Description:	Check Token Validation
-- =============================================
CREATE PROCEDURE [dbo].[SP_UserToken_Validation]
	@Token varchar(36),	
	@UserId int out,

	@ErrorCode int out,				--200 = OK, else error code based on restful codes
	@ErrorMessage varchar(4000) out	--empty = OK, else error message
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	set @UserId = (
		select top 1 t.UserId
		from
			v_UserTokens t inner join v_Users u on t.UserId = u.UserId
		where
			t.Token = @Token
	)

	if @UserId is null
	begin
		set @ErrorCode = 401
		set @ErrorMessage = 'Invalid Token'
		return
	end

	set @ErrorCode = 200
	set @ErrorMessage = ''
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'API inputs/results as json' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APILog', @level2type=N'COLUMN',@level2name=N'Params'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If the API result not OK, save the error message (if exist).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APILog', @level2type=N'COLUMN',@level2name=N'ErrorMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The API result status code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APILog', @level2type=N'COLUMN',@level2name=N'StatusCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Save all API inputs/results for tracking and maintaining.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'APILog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Application Menu
2 = restful API' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu', @level2type=N'COLUMN',@level2name=N'Kind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application menus and restful APIs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Menu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'List of users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Menu"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 241
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Menu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Menu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RoleAccessMenu"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 295
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_RoleAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_RoleAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Roles"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 150
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Roles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Roles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserAccessMenu"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 292
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserRoles"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 271
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserRoles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 244
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_Users'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserTokens"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 227
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserTokens'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_UserTokens'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[29] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "m"
            Begin Extent = 
               Top = 11
               Left = 405
               Bottom = 264
               Right = 815
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 208
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_RoleAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_RoleAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 203
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "M"
            Begin Extent = 
               Top = 13
               Left = 460
               Bottom = 232
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_UserAccessMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_UserAccessMenu'
GO
USE [master]
GO
ALTER DATABASE [FTSS] SET  READ_WRITE 
GO
