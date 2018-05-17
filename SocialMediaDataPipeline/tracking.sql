DROP TABLE [JobInfo]
CREATE TABLE [dbo].[JobInfo](
	[JobRunId] int identity(1,1),
	[Platform] [nvarchar](50) NULL,
	[MaxUpdate] DATETIME NULL,
	[JobStartAt] [datetime] NULL,
	[JobEndAt] [datetime] NULL,
    [Status] [nvarchar](20) NULL,
	[IsNotified] bit null,
	ErrorMessage nvarchar(max) null,
 CONSTRAINT [PK_JobInfo] PRIMARY KEY CLUSTERED 
(
	[JobRunId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

select GETUTCDATE()

insert into jobinfo
select 'Github','2018-05-16 23:00:00',GETUTCDATE(),DATEADD(day,1,getutcdate()),'Completed',1,null
INSERT INTO JobInfo SELECT 'GitHub','5/17/2018 12:00:00 AM','5/17/2018 10:28:11 AM',null,'InProgress',0,null

select * from jobinfo
DELETE JOBINFO WHERE JOBRUNID=2