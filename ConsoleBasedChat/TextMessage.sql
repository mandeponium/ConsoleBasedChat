SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TextMessage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MessageBody] [nvarchar](max) NULL,
	[MessageTime] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TextMessage] ADD  CONSTRAINT [DF_Message]  DEFAULT (getdate()) FOR [MessageTime]
GO


