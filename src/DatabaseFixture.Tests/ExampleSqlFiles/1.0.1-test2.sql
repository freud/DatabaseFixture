CREATE TABLE [dbo].[Events](
    [LogId] [int] NOT NULL,
    [OrderId] [int] NOT NULL,
    [Data] [nvarchar](max) NOT NULL,
    [SyncedAt] [datetime] NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ( [LogId] ASC )
)