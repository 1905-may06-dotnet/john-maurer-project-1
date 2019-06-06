SET QUOTED_IDENTIFIER OFF;
GO
USE [model];
GO
IF SCHEMA_ID(N'PizzaBoxDbSchema') IS NULL EXECUTE(N'CREATE SCHEMA [PizzaBoxDbSchema]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_PersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Addresses] DROP CONSTRAINT [FK_PersonAddress];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_OutletAddress]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Addresses] DROP CONSTRAINT [FK_OutletAddress];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_OutletOrder]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Orders] DROP CONSTRAINT [FK_OutletOrder];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_PersonOrder]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Orders] DROP CONSTRAINT [FK_PersonOrder];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_OutletFeature]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Features] DROP CONSTRAINT [FK_OutletFeature];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_OutletItem]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Items] DROP CONSTRAINT [FK_OutletItem];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_EmployeeOutlet]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Employees] DROP CONSTRAINT [FK_EmployeeOutlet];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[FK_EmployeePerson]', 'F') IS NOT NULL
    ALTER TABLE [PizzaBoxDbSchema].[Employees] DROP CONSTRAINT [FK_EmployeePerson];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[PizzaBoxDbSchema].[People]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[People];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Addresses];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Features]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Features];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Items]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Items];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Orders]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Orders];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[StateTaxes]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[StateTaxes];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Outlets]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Outlets];
GO
IF OBJECT_ID(N'[PizzaBoxDbSchema].[Employees]', 'U') IS NOT NULL
    DROP TABLE [PizzaBoxDbSchema].[Employees];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [PizzaBoxDbSchema].[People] (
    [Id] uniqueidentifier  NOT NULL,
    [FName] nvarchar(max)  NOT NULL,
    [MName] nvarchar(max)  NULL,
    [LName] nvarchar(max)  NOT NULL,
    [Gender] bit  NULL,
    [DoB] datetime  NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [PizzaBoxDbSchema].[Addresses] (
    [Id] uniqueidentifier  NOT NULL,
    [PersonId] uniqueidentifier  NULL,
    [OutletId] uniqueidentifier  NULL,
    [State] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL,
    [Apt] nvarchar(max)  NULL
);
GO

-- Creating table 'Features'
CREATE TABLE [PizzaBoxDbSchema].[Features] (
    [Id] uniqueidentifier  NOT NULL,
    [OutletId] uniqueidentifier  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Cost] float  NOT NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [PizzaBoxDbSchema].[Items] (
    [Id] uniqueidentifier  NOT NULL,
    [OutletId] uniqueidentifier  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Cost] float  NOT NULL,
    [Features] nvarchar(max)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [PizzaBoxDbSchema].[Orders] (
    [Id] uniqueidentifier  NOT NULL,
    [OutletId] uniqueidentifier  NULL,
    [PersonId] uniqueidentifier  NULL,
    [DateOrdered] datetime  NOT NULL,
    [Items] nvarchar(max)  NOT NULL,
    [SubTotal] float  NOT NULL,
    [Total] float  NOT NULL
);
GO

-- Creating table 'StateTaxes'
CREATE TABLE [PizzaBoxDbSchema].[StateTaxes] (
    [Id] uniqueidentifier  NOT NULL,
    [Territory] nvarchar(max)  NOT NULL,
    [TaxRate] float  NOT NULL
);
GO

-- Creating table 'Outlets'
CREATE TABLE [PizzaBoxDbSchema].[Outlets] (
    [Id] uniqueidentifier  NOT NULL,
    [Organization] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [PizzaBoxDbSchema].[Employees] (
    [Id] uniqueidentifier  NOT NULL,
    [OutletId] uniqueidentifier  NULL,
    [Position] nvarchar(max)  NOT NULL,
    [WageType] nvarchar(max)  NOT NULL,
    [Wage] float  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Person_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [PizzaBoxDbSchema].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [PizzaBoxDbSchema].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Features'
ALTER TABLE [PizzaBoxDbSchema].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Items'
ALTER TABLE [PizzaBoxDbSchema].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [PizzaBoxDbSchema].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StateTaxes'
ALTER TABLE [PizzaBoxDbSchema].[StateTaxes]
ADD CONSTRAINT [PK_StateTaxes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Outlets'
ALTER TABLE [PizzaBoxDbSchema].[Outlets]
ADD CONSTRAINT [PK_Outlets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [PizzaBoxDbSchema].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonId] in table 'Addresses'
ALTER TABLE [PizzaBoxDbSchema].[Addresses]
ADD CONSTRAINT [FK_PersonAddress]
    FOREIGN KEY ([PersonId])
    REFERENCES [PizzaBoxDbSchema].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAddress'
CREATE INDEX [IX_FK_PersonAddress]
ON [PizzaBoxDbSchema].[Addresses]
    ([PersonId]);
GO

-- Creating foreign key on [OutletId] in table 'Addresses'
ALTER TABLE [PizzaBoxDbSchema].[Addresses]
ADD CONSTRAINT [FK_OutletAddress]
    FOREIGN KEY ([OutletId])
    REFERENCES [PizzaBoxDbSchema].[Outlets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OutletAddress'
CREATE INDEX [IX_FK_OutletAddress]
ON [PizzaBoxDbSchema].[Addresses]
    ([OutletId]);
GO

-- Creating foreign key on [OutletId] in table 'Orders'
ALTER TABLE [PizzaBoxDbSchema].[Orders]
ADD CONSTRAINT [FK_OutletOrder]
    FOREIGN KEY ([OutletId])
    REFERENCES [PizzaBoxDbSchema].[Outlets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OutletOrder'
CREATE INDEX [IX_FK_OutletOrder]
ON [PizzaBoxDbSchema].[Orders]
    ([OutletId]);
GO

-- Creating foreign key on [PersonId] in table 'Orders'
ALTER TABLE [PizzaBoxDbSchema].[Orders]
ADD CONSTRAINT [FK_PersonOrder]
    FOREIGN KEY ([PersonId])
    REFERENCES [PizzaBoxDbSchema].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonOrder'
CREATE INDEX [IX_FK_PersonOrder]
ON [PizzaBoxDbSchema].[Orders]
    ([PersonId]);
GO

-- Creating foreign key on [OutletId] in table 'Features'
ALTER TABLE [PizzaBoxDbSchema].[Features]
ADD CONSTRAINT [FK_OutletFeature]
    FOREIGN KEY ([OutletId])
    REFERENCES [PizzaBoxDbSchema].[Outlets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OutletFeature'
CREATE INDEX [IX_FK_OutletFeature]
ON [PizzaBoxDbSchema].[Features]
    ([OutletId]);
GO

-- Creating foreign key on [OutletId] in table 'Items'
ALTER TABLE [PizzaBoxDbSchema].[Items]
ADD CONSTRAINT [FK_OutletItem]
    FOREIGN KEY ([OutletId])
    REFERENCES [PizzaBoxDbSchema].[Outlets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OutletItem'
CREATE INDEX [IX_FK_OutletItem]
ON [PizzaBoxDbSchema].[Items]
    ([OutletId]);
GO

-- Creating foreign key on [OutletId] in table 'Employees'
ALTER TABLE [PizzaBoxDbSchema].[Employees]
ADD CONSTRAINT [FK_EmployeeOutlet]
    FOREIGN KEY ([OutletId])
    REFERENCES [PizzaBoxDbSchema].[Outlets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeeOutlet'
CREATE INDEX [IX_FK_EmployeeOutlet]
ON [PizzaBoxDbSchema].[Employees]
    ([OutletId]);
GO

-- Creating foreign key on [Person_Id] in table 'Employees'
ALTER TABLE [PizzaBoxDbSchema].[Employees]
ADD CONSTRAINT [FK_EmployeePerson]
    FOREIGN KEY ([Person_Id])
    REFERENCES [PizzaBoxDbSchema].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeePerson'
CREATE INDEX [IX_FK_EmployeePerson]
ON [PizzaBoxDbSchema].[Employees]
    ([Person_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

-- --------------------------------------------------
-- State/Territory Inserts
-- --------------------------------------------------

-- Source: http://www.tax-rates.org/taxtables/sales-tax-by-state

INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "AL", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "AK", 0.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "AZ", 5.6 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "AR", 6.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "CA", 7.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "CO", 2.9 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "CT", 6.35 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "DE", 0.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "FL", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "GA", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "HI", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "ID", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "IL", 6.25 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "IN", 7.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "IA", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "KS", 6.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "KY", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "LA", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "ME", 5.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MD", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MA", 6.25 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MI", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MN", 6.88 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MS", 7.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MO", 4.23 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "MT", 0.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NE", 5.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NV", 6.85 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NH", 0.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NJ", 7.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NM", 5.13 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NY", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "NC", 4.75 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "ND", 5.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "OH", 5.75 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "OK", 4.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "OR", 0.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "PA", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "RI", 7.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "SC", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "SD", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "TN", 7.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "TX", 6.25 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "UT", 5.95 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "VT", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "VA", 5.3 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "WA", 6.5 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "WV", 6.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "WI", 5.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "WY", 4.0 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "DC", 5.75 );
INSERT INTO [PizzaBoxDbSchema].[StateTaxes] VALUES ( NEWID (), "PR", 6.0 );

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------