CREATE TABLE UpdateLog
(
    Id int PRIMARY KEY IDENTITY,
    Title nvarchar(20),
    Log nvarchar(200),
    UpdateDate datetime
);

