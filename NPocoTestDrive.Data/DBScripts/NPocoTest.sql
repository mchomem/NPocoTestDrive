use master
go

if not exists(select * from sys.databases where name = 'NPocoTest')
begin
	create database NPocoTest
end

use NPocoTest
go

declare @reset bit = 1

if @reset = 1
begin
	if exists(select * from sys.tables where name = 'Employee')
	begin
		drop table Employee	

		create table Employee
		(
			EmployeeID int not null identity(1,1)
			, [Name] nvarchar(100) not null
			, DocumentNumber nvarchar(14) not null
			, Active bit not null
			, CreatedIn datetime not null
			, UpdatedIn datetime null
		)
	end
	else
	begin
		create table Employee
		(
			EmployeeID int not null identity(1,1)
			, [Name] nvarchar(100) not null
			, DocumentNumber nvarchar(14) not null
			, Active bit not null
			, CreatedIn datetime not null
			, UpdatedIn datetime null
		)
	end

	alter table Employee add constraint PK_Employee primary key(EmployeeID)
end
go

declare @clearRows bit = 0

if (@clearRows = 1)
begin
	truncate table Employee
	DBCC CHECKIDENT ('Employee', RESEED, 1);
end


declare @tabNames table (id int identity(1,1), [name] nvarchar(50))
insert into @tabNames values('Andre'),( 'Bruno'),( 'Carlos'),( 'Daniel'),( 'Eduardo'),( 'Fabrício'),( 'Gabriel'),( 'Heron'),( 'Israel'),( 'João'),( 'Leonarndo'),( 'Marcos')

declare @tabSecondNames table (id int identity(1,1), [name] nvarchar(50))
insert into @tabSecondNames values('Arnaldo'),( 'Batista'),( 'Canto'),( 'Dantes'),( 'Estevão'),( 'Farçante'),( 'Galão'),( 'Hebron'),( 'Istanbu'),( 'Jarel'),( 'Lemos'),( 'Marcondes')


declare @totalRecords int = 1000

while @totalRecords != 0
begin
	set @totalRecords = @totalRecords - 1

	-- Monta o nome completo
	declare @fullName nvarchar(100) =
		(select [name] from @tabNames where id = (select abs(checksum(newid()) % (select count(*) from @tabNames)) + 1))
		+ ' ' + (select [name] from @tabSecondNames where id = (select abs(checksum(newid()) % (select count(*) from @tabSecondNames)) + 1))

	declare @documentNumber nvarchar(14) = ''

	-- Gera um cpf qualquer de CPF (com máscara)
	while len(@documentNumber) < 14
	begin
		set @documentNumber = @documentNumber + cast( (select abs(checksum(newid()) % 10)) as nvarchar)

		if(len(@documentNumber) = 3)
		begin
			set @documentNumber = @documentNumber + '.'	
		end
		else if(len(@documentNumber) = 7)
		begin
			set @documentNumber = @documentNumber + '.'	
		end
		else if(len(@documentNumber) = 11)
		begin
			set @documentNumber = @documentNumber + '-'
		end
	end

	-- Define aleatoreamente se o colaborador estará ativo ou não (margem de 50%).
	declare @active bit = iif( (select abs(checksum(newid()) % 10) + 1 ) >= 5 , 1, 0)

	-- Gera uma data aleatória
	declare @dateCreatedIn datetime
	declare @day nvarchar(2) = (select abs(checksum(newid()) % 28)) + 1
	declare @month nvarchar(2) = (select abs(checksum(newid()) % 12)) + 1
	declare @year nvarchar(4) = (year(getdate()) - (select abs(checksum(newid()) % 50)))
	declare @hour nvarchar(2) = (select abs(checksum(newid()) % 23)) + 1
	declare @minute nvarchar(2) = (select abs(checksum(newid()) % 59)) + 1
	declare @second nvarchar(2) = (select abs(checksum(newid()) % 59)) + 1
	set @dateCreatedIn = (cast( (@year + '-' + @month + '-' + @day + ' ' + @hour + ':' + @minute + ':' + @second) as datetime))

	insert into Employee
	(
		[Name]
		, DocumentNumber
		, Active
		, CreatedIn
	)
	values
	(
		@fullName
		, @documentNumber
		, @active
		, @dateCreatedIn
	)	
end

select * from Employee
