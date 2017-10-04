﻿using SimpleMigrations;

namespace Netsphere.Database.Migration.Auth
{
    [Migration(1)]
    public class Base : SimpleMigrations.Migration
    {
        protected override void Up()
        {
            Execute(@"CREATE TABLE `accounts` (
              `Id` int(11) NOT NULL AUTO_INCREMENT,
              `Username` varchar(40) CHARACTER SET utf8 NOT NULL,
              `Nickname` varchar(40) CHARACTER SET utf8 DEFAULT NULL,
              `Password` varchar(40) CHARACTER SET utf8 DEFAULT NULL,
              `Salt` varchar(40) CHARACTER SET utf8 DEFAULT NULL,
              `SecurityLevel` tinyint(3) unsigned NOT NULL DEFAULT '0',
              PRIMARY KEY (`Id`),
              UNIQUE KEY `Username` (`Username`),
              UNIQUE KEY `Nickname` (`Nickname`)
            );");
            
            Execute(@"CREATE TABLE `bans` (
              `Id` int(11) NOT NULL AUTO_INCREMENT,
              `AccountId` int(11) NOT NULL,
              `Date` bigint(20) NOT NULL DEFAULT '0',
              `Duration` bigint(20) DEFAULT NULL,
              `Reason` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
              PRIMARY KEY (`Id`),
              KEY `AccountId` (`AccountId`),
              CONSTRAINT `bans_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
            );");
            
            Execute(@"CREATE TABLE `login_history` (
              `Id` int(11) NOT NULL AUTO_INCREMENT,
              `AccountId` int(11) NOT NULL,
              `Date` bigint(20) NOT NULL DEFAULT '0',
              `IP` varchar(15) COLLATE utf8_bin DEFAULT NULL,
              PRIMARY KEY (`Id`),
              KEY `AccountId` (`AccountId`),
              CONSTRAINT `login_history_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
            );");
            
            Execute(@"CREATE TABLE `nickname_history` (
              `Id` int(11) NOT NULL AUTO_INCREMENT,
              `AccountId` int(11) NOT NULL,
              `Nickname` varchar(40) CHARACTER SET utf8 NOT NULL,
              `ExpireDate` bigint(20) DEFAULT NULL,
              PRIMARY KEY (`Id`),
              KEY `AccountId` (`AccountId`),
              CONSTRAINT `nickname_history_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
            );");
        }

        protected override void Down()
        {
            Execute("DROP TABLE IF EXISTS `nickname_history`;");
            Execute("DROP TABLE IF EXISTS `login_history`;");
            Execute("DROP TABLE IF EXISTS `bans`;");
            Execute("DROP TABLE IF EXISTS `accounts`;");
        }
    }
}
