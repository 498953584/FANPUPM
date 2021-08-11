create FUNCTION [dbo].[fnGetDistance](@LngBegin DECIMAL(18,14),@LatBegin DECIMAL(18,14), @LngEnd DECIMAL(18,14), @LatEnd DECIMAL(18,14)) RETURNS INT
AS
BEGIN
--获取经纬度两点之间距离 单位：米
DECLARE @Distance DECIMAL(12,5)
DECLARE @EARTH_RADIUS DECIMAL(12,4)
SET @EARTH_RADIUS = 6378137.0
DECLARE @DecLatBegin DECIMAL(18,14),@DecLatEnd DECIMAL(18,14),@DecLatDiff DECIMAL(18,14),@DecLngDiff DECIMAL(18,14)
SET @DecLatBegin = @LatBegin *PI()/180.0
SET @DecLatEnd = @LatEnd *PI()/180.0
SET @DecLatDiff = @DecLatBegin - @DecLatEnd
SET @DecLngDiff = @LngBegin *PI()/180.0 - @LngEnd *PI()/180.0
SET @Distance = 2 *ASIN(SQRT(POWER(SIN(@DecLatDiff/2), 2)+COS(@DecLatBegin)*COS(@DecLatEnd)*POWER(SIN(@DecLngDiff/2), 2)))
SET @Distance = @Distance * @EARTH_RADIUS
SET @Distance = Round((@Distance * 10000),5) / 10000
RETURN @Distance
END