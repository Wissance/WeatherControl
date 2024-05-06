USE [Wissance.WeatherControl]
GO

INSERT INTO [dbo].[Station]([Name] ,[Description] ,[Longitude] ,[Latitude])
     VALUES (N'Северн-1', 'Метеостанция на горе Белая', '59.42.59', '57.39.17');
INSERT INTO [dbo].[Station]([Name] ,[Description] ,[Longitude] ,[Latitude])
     VALUES (N'Юг-1', 'Метеостанция г. Магнитогорск', '59.02.00', '53.23.00');
INSERT INTO [dbo].[Station]([Name] ,[Description] ,[Longitude] ,[Latitude])
     VALUES (N'Центр-1', 'Метеостанция г. Екатеринбург', '63.50', '56.50');
INSERT INTO [dbo].[Station]([Name] ,[Description] ,[Longitude] ,[Latitude])
     VALUES (N'Запад-1', 'Метеостанция г. Уфа', '55.58', '54.44');
INSERT INTO [dbo].[Station]([Name] ,[Description] ,[Longitude] ,[Latitude])
     VALUES (N'Восток-1', 'Метеостанция г. Тюмень', '65.32', '59.09');

GO


