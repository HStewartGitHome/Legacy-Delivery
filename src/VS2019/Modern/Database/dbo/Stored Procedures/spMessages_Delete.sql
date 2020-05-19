CREATE PROCEDURE [dbo].[spMessages_Delete]
	@Id int
AS
begin
	set nocount on;

	delete from dbo.Messages
	where Id = @Id;
end
