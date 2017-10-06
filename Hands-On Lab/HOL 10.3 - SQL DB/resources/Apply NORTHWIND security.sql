CREATE FUNCTION [Security].[fn_securitypredicate](@EmployeeName AS sysname)  
    RETURNS TABLE  
WITH SCHEMABINDING  
AS  
    RETURN SELECT 1 AS fn_securitypredicate_result   
WHERE @EmployeeName = USER_NAME() OR USER_NAME() = 'Janet' OR USER_NAME() = 'dbo';
GO

CREATE SCHEMA [Security]
GO

CREATE SECURITY POLICY OrdersFilter  
ADD FILTER PREDICATE Security.fn_securitypredicate(EmployeeName)   
ON dbo.Orders  
WITH (STATE = ON); 

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO