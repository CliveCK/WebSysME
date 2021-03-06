
/****** Object:  StoredProcedure [dbo].[sp_Save_Files]    Script Date: 10/11/2015 10:12:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_Save_Files] 
( 
	@FileID int = null output,  
	@UpdatedBy int = null, 
	@Date datetime = null, 
	@FileTypeID int = null,
	@Title varchar(150) = null,
	@Author varchar(150) = null, 
	@Description varchar(255) = null, 
	@FilePath varchar(255) = null, 
	@FileExtension varchar(50) = null, 
	@AuthorOrganization varchar(150) = null,
	@ApplySecurity int = null
) 
AS 
BEGIN 
 
	IF @FileID IS NULL OR @FileID <= 0 
	BEGIN 
		 
		SELECT @FileID = NULL 
 
		INSERT INTO [tblFiles](
			[Date], 
			[FileTypeID],
			[Title],
			[Author], 
			[Description], 
			[FilePath], 
			[FileExtension], 
			[AuthorOrganization], [ApplySecurity],			[CreatedDate], 			[CreatedBy]
		) VALUES ( 

			@Date, 
			@FileTypeID, 
			@Title,
			@Author, 
			@Description, 
			@FilePath, 
			@FileExtension, 
			@AuthorOrganization,
			@ApplySecurity, 
			getdate(), 
			@UpdatedBy
		) 
	END 
	ELSE 
	BEGIN 
		UPDATE [tblFiles] SET  
			[UpdatedBy]=@UpdatedBy, 
			[Date]=@Date, 
			[FileTypeID]=@FileTypeID, 
			[Title]=@Title,
			[Author]=@Author, 
			[Description]=@Description, 
			[FilePath]=@FilePath, 
			[FileExtension]=@FileExtension, 
			[AuthorOrganization]=@AuthorOrganization, 
			[ApplySecurity]=@ApplySecurity,
			[UpdatedDate] = getdate()

		WHERE [FileID]=@FileID 
	END 
 
	SELECT @FileID = ISNULL(@FileID, SCOPE_IDENTITY()) 
	SELECT * FROM (SELECT @FileID AS ReturnID) AS FilesReturnTable 
 
	RETURN @FileID 
 
END 