		function IsInteger(objValue)
		{
			if (objValue!="")
			{
				if (!(new RegExp(/^\d+$/).test(objValue)))
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return false;
			}
		}
			
		function IsDoubleType(moneyValue)
		{
			var countPoint = 0;
			if( (moneyValue.length==0)||(moneyValue.length>30))
			{
				return false;
			}
	
			for( j = 0; j < moneyValue.length; j++ )
			{
				//验证非法字符
				if( (moneyValue.charAt(j) < '0' || moneyValue.charAt(j) > '9') && moneyValue.charAt(j) != '.' )
				{
				return false;
				}
				//验证点号是否过多
				if( moneyValue.charAt(j) == '.' )
				countPoint++;	
				if( countPoint > 1 )
				{
					return false;
				}
			}
	
			//验证点号前如果是零的话，点号后至少有一位
			var point = moneyValue.indexOf('.');
			if( point == 0 )
			{
				return false;
			}
			if( moneyValue.length == point+1 )
			{
				return false;
			}
	
			return true;
		}