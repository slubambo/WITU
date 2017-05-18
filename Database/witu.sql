/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : witu

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2017-05-18 16:38:54
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `access_role`
-- ----------------------------
DROP TABLE IF EXISTS `access_role`;
CREATE TABLE `access_role` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL COMMENT 'The level beyond which the user has no priveledge to access; University, Campus, Faculty, Department, Program, Specialisation, Course',
  `levelId` int(10) unsigned NOT NULL COMMENT 'Level Id, can be University, Campus, Faculty, Department, Program, Specialisation or Course',
  `levelName` varchar(100) NOT NULL COMMENT 'The Name of that Level; helps in reducing the chaos of searching for the corresponding Name',
  PRIMARY KEY (`id`),
  KEY `FKAccessRoleUser` (`userId`),
  KEY `userId` (`userId`),
  CONSTRAINT `FK6F40428665A1D7D8` FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  CONSTRAINT `FKAccessRoleUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Works in hand with Roles to authenticate users who want to a';

-- ----------------------------
-- Records of access_role
-- ----------------------------

-- ----------------------------
-- Table structure for `audit`
-- ----------------------------
DROP TABLE IF EXISTS `audit`;
CREATE TABLE `audit` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `IpAddress` varchar(255) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `UrlAccessed` varchar(255) NOT NULL,
  `TimeAccessed` datetime NOT NULL,
  `Data` longtext,
  `Message` varchar(500) DEFAULT NULL,
  `UserType` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of audit
-- ----------------------------

-- ----------------------------
-- Table structure for `campus`
-- ----------------------------
DROP TABLE IF EXISTS `campus`;
CREATE TABLE `campus` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(150) NOT NULL,
  `shortName` varchar(150) DEFAULT NULL,
  `instituteId` int(10) unsigned DEFAULT NULL,
  `code` varchar(50) DEFAULT NULL,
  `description` text COMMENT 'More like a brief summary about the university. ',
  PRIMARY KEY (`id`),
  KEY `campusInstitute` (`instituteId`) USING BTREE,
  CONSTRAINT `FKCampusInstitute` FOREIGN KEY (`instituteId`) REFERENCES `institution` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='Stores campuses in a university';

-- ----------------------------
-- Records of campus
-- ----------------------------
INSERT INTO `campus` VALUES ('1', 'Kampala', 'KLA', null, null, null);
INSERT INTO `campus` VALUES ('2', 'Mbarara', 'MBRA', null, null, null);

-- ----------------------------
-- Table structure for `country`
-- ----------------------------
DROP TABLE IF EXISTS `country`;
CREATE TABLE `country` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `nationality` varchar(100) DEFAULT NULL,
  `countryCode` varchar(40) DEFAULT NULL,
  `createdOn` datetime DEFAULT NULL,
  `lastModified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=256 DEFAULT CHARSET=latin1 COMMENT='Stores all the countries in the world. Is used as a referenc';

-- ----------------------------
-- Records of country
-- ----------------------------
INSERT INTO `country` VALUES ('1', 'Akrotiri', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('2', 'Albania', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('3', 'Algeria', 'Algerian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('4', 'American Samoa', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('5', 'Andorra', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('6', 'Angola', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('7', 'Anguilla', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('8', 'Antarctica', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('9', 'Antigua and Barbuda', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('10', 'Argentina', 'Argentine', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('11', 'Armenia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('12', 'Aruba', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('13', 'Ashmore and Cartier Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('14', 'Australia', 'Australian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('15', 'Austria', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('16', 'Azerbaijan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('17', 'Bahamas, The', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('18', 'Bahrain', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('19', 'Bangladesh', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('20', 'Barbados', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('21', 'Bassas da India', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('22', 'Belarus', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('23', 'Belgium', 'Belgian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('24', 'Belize', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('25', 'Benin', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('26', 'Bermuda', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('27', 'Bhutan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('28', 'Bolivia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('29', 'Bosnia and Herzegovina', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('30', 'Botswana', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('31', 'Bouvet Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('32', 'Brazil', 'Brazilian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('33', 'British Indian Ocean Territory', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('34', 'British Virgin Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('35', 'Brunei', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('36', 'Bulgaria', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('37', 'Burkina Faso', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('38', 'Burma', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('39', 'Burundi', 'Burundian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('40', 'Cambodia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('41', 'Cameroon', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('42', 'Canada', 'Canadian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('43', 'Cape Verde', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('44', 'Cayman Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('45', 'Central African Republic', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('46', 'Chad', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('47', 'Chile', 'Chilean', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('48', 'China', 'Chinese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('49', 'Christmas Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('50', 'Clipperton Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('51', 'Cocos (Keeling) Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('52', 'Colombia', 'Colombian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('53', 'Comoros', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('54', 'Congo, Democratic Republic of the', 'Congolese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('55', 'Congo, Republic of the', 'Congolese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('56', 'Cook Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('57', 'Coral Sea Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('58', 'Costa Rica', 'Costa Rican', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('59', 'Cote d\'Ivoire', 'Ivorian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('60', 'Croatia', 'Croatian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('61', 'Cuba', 'Cuban', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('62', 'Cyprus', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('63', 'Czech Republic', 'Czech', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('64', 'Denmark', 'Dane', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('65', 'Dhekelia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('66', 'Djibouti', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('67', 'Dominica', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('68', 'Dominican Republic', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('69', 'Ecuador', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('70', 'Egypt', 'Egyptian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('71', 'El Salvador', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('72', 'Equatorial Guinea', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('73', 'Eritrea', 'Eritrean', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('74', 'Estonia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('75', 'Ethiopia', 'Ethiopian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('76', 'Europa Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('77', 'Falkland Islands (Islas Malvinas)', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('78', 'Faroe Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('79', 'Fiji', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('80', 'Finland', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('81', 'France', 'French', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('82', 'French Guiana', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('83', 'French Polynesia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('84', 'French Southern and Antarctic Lands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('85', 'Gambia, The', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('86', 'Gaza Strip', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('87', 'Georgia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('88', 'Germany', 'German', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('89', 'Ghana', 'Ghananian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('90', 'Gibraltar', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('91', 'Glorioso Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('92', 'Greece', 'Greek', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('93', 'Greenland', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('94', 'Grenada', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('95', 'Guadeloupe', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('96', 'Guam', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('97', 'Guatemala', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('98', 'Guernsey', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('99', 'Guinea', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('100', 'Guinea-Bissau', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('101', 'Guyana', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('102', 'Haiti', 'Haitian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('103', 'Heard Island and McDonald Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('104', 'Holy See (Vatican City)', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('105', 'Honduras', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('106', 'Hong Kong', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('107', 'Hungary', 'Hungarian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('108', 'Iceland', 'Icelander', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('109', 'India', 'Indian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('110', 'Indonesia', 'Indonesian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('111', 'Iran', 'Iranian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('112', 'Iraq', 'Iraqi', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('113', 'Ireland', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('114', 'Isle of Man', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('115', 'Israel', 'Israeli', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('116', 'Italy', 'Italian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('117', 'Jamaica', 'Jamaican', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('118', 'Jan Mayen', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('119', 'Japan', 'Japanese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('120', 'Jersey', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('121', 'Jordan', 'Jordanian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('122', 'Juan de Nova Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('123', 'Kazakhstan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('124', 'Kenya', 'Kenyan', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('125', 'Kiribati', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('126', 'Korea, North', 'North Korean', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('127', 'Korea, South', 'South Korean', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('128', 'Kuwait', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('129', 'Kyrgyzstan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('130', 'Laos', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('131', 'Latvia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('132', 'Lebanon', 'Lebanese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('133', 'Lesotho', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('134', 'Liberia', 'Liberian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('135', 'Libya', 'Libyan', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('136', 'Liechtenstein', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('137', 'Lithuania', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('138', 'Luxembourg', 'Luxembourger', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('139', 'Macau', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('140', 'Macedonia', 'Macedonian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('141', 'Madagascar', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('142', 'Malawi', 'Malawian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('143', 'Malaysia', 'Malaysian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('144', 'Maldives', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('145', 'Mali', 'Malian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('146', 'Malta', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('147', 'Marshall Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('148', 'Martinique', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('149', 'Mauritania', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('150', 'Mauritius', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('151', 'Mayotte', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('152', 'Mexico', 'Mexican', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('153', 'Micronesia, Federated States of', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('154', 'Moldova', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('155', 'Monaco', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('156', 'Mongolia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('157', 'Montserrat', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('158', 'Morocco', 'Moroccan', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('159', 'Mozambique', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('160', 'Namibia', 'Namibian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('161', 'Nauru', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('162', 'Navassa Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('163', 'Nepal', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('164', 'Netherlands', 'Dutch', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('165', 'Netherlands Antilles', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('166', 'New Caledonia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('167', 'New Zealand', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('168', 'Nicaragua', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('169', 'Niger', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('170', 'Nigeria', 'Nigerian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('171', 'Niue', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('172', 'Norfolk Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('173', 'Northern Mariana Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('174', 'Norway', 'Norwegian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('175', 'Oman', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('176', 'Pakistan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('177', 'Palau', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('178', 'Panama', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('179', 'Papua New Guinea', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('180', 'Paracel Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('181', 'Paraguay', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('182', 'Peru', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('183', 'Philippines', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('184', 'Pitcairn Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('185', 'Poland', 'Polish', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('186', 'Portugal', 'Portuguese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('187', 'Puerto Rico', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('188', 'Qatar', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('189', 'Reunion', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('190', 'Romania', 'Romanian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('191', 'Russia', 'Russian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('192', 'Rwanda', 'Rwandese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('193', 'Saint Helena', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('194', 'Saint Kitts and Nevis', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('195', 'Saint Lucia', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('196', 'Saint Pierre and Miquelon', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('197', 'Saint Vincent and the Grenadines', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('198', 'Samoa', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('199', 'San Marino', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('200', 'Sao Tome and Principe', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('201', 'Saudi Arabia', 'Saudi', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('202', 'Senegal', 'Senegalese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('203', 'Serbia and Montenegro', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('204', 'Seychelles', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('205', 'Sierra Leone', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('206', 'Singapore', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('207', 'Slovakia', 'Slovak', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('208', 'Slovenia', 'Slovene', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('209', 'Solomon Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('210', 'Somalia', 'Somalian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('211', 'South Africa', 'South African', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('212', 'South Georgia and the South Sandwich Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('213', 'Spain', 'Spanish', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('214', 'Spratly Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('215', 'Sri Lanka', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('216', 'Sudan', 'Sudanese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('217', 'Suriname', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('218', 'Svalbard', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('219', 'Swaziland', 'Swazi', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('220', 'Sweden', 'Swedish', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('221', 'Switzerland', 'Swiss', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('222', 'Syria', 'Syrian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('223', 'Taiwan', 'Taiwanese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('224', 'Tajikistan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('225', 'Tanzania', 'Tanzanian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('226', 'Thailand', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('227', 'Timor-Leste', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('228', 'Togo', 'Togolese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('229', 'Tokelau', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('230', 'Tonga', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('231', 'Trinidad and Tobago', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('232', 'Tromelin Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('233', 'Tunisia', 'Tunisian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('234', 'Turkey', 'Turkish', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('235', 'Turkmenistan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('236', 'Turks and Caicos Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('237', 'Tuvalu', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('238', 'United Arab Emirates', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('239', 'United Kingdom', 'English', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('240', 'United States', 'American', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('241', 'Uruguay', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('242', 'Uzbekistan', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('243', 'Vanuatu', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('244', 'Venezuela', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('245', 'Vietnam', 'Vietnamese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('246', 'Virgin Islands', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('247', 'Wake Island', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('248', 'Wallis and Futuna', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('249', 'West Bank', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('250', 'Western Sahara', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('251', 'Yemen', '', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('252', 'Zambia', 'Zambian', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('253', 'Zimbabwe', 'Zimbabwean', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('254', 'South Sudan', 'S. Sudanese', '', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `country` VALUES ('255', 'Uganda', 'Ugandan', 'Ug', '2014-04-17 16:31:33', '2014-10-08 13:09:39');

-- ----------------------------
-- Table structure for `county`
-- ----------------------------
DROP TABLE IF EXISTS `county`;
CREATE TABLE `county` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `districtId` int(10) unsigned NOT NULL,
  `code` varchar(50) DEFAULT NULL,
  `createdOn` datetime DEFAULT NULL,
  `lastModified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKCountyDistrict` (`districtId`),
  KEY `districtId` (`districtId`),
  CONSTRAINT `FK7724302827765E8C` FOREIGN KEY (`districtId`) REFERENCES `district` (`id`),
  CONSTRAINT `FKCountyDistrict` FOREIGN KEY (`districtId`) REFERENCES `district` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=160 DEFAULT CHARSET=latin1 COMMENT='Lists all counties of Uganda. This is meant to be a referenc';

-- ----------------------------
-- Records of county
-- ----------------------------
INSERT INTO `county` VALUES ('1', 'AGULE', '82', '35-01', '2014-10-08 13:09:39', '2014-10-08 13:09:39');
INSERT INTO `county` VALUES ('2', 'AMURIA', '1', '58-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('3', 'ARINGA', '93', '53-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('4', 'ARUU', '81', '50-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('5', 'ASWA', '21', '5-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('6', 'BAMUNANIKA', '51', '23-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('7', 'BBALE', '32', '47-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('8', 'BOKORA', '73', '107-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('9', 'BUBULO', '54', '67-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('10', 'BUDADIRI', '88', '51-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('11', 'BUDIOPE EAST', '18', '83-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('12', 'BUDIOPE WEST', '18', '83-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('13', 'BUDYEBO', '70', '44-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('14', 'BUFUMBIRA', '38', '18-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('15', 'BUGABULA', '29', '13-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('16', 'BUGANGAIZI EAST', '33', '16-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('17', 'BUGANGAIZI WEST', '33', '16-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('18', 'BUGHENDERA', '12', '3-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('19', 'BUGWERI', '23', '7-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('20', 'BUHWEJU', '6', '109-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('21', 'BUIKWE', '7', '82-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('22', 'BUJENJE', '57', '25-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('23', 'BUKANGA', '24', '62-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('24', 'BUKOMANSIMBI', '8', '98-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('25', 'BUKOOLI', '5', '41-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('26', 'BUKOOLI ISLAND', '71', '95-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('27', 'BUKOOLI SOUTH', '71', '95-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('28', 'BUKOTO', '56', '24-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('29', 'BULAMBULI', '10', '89-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('30', 'BULAMOGI', '27', '64-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('31', 'BULIISA', '11', '73-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('32', 'BUNGOKHO', '59', '26-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('33', 'BUNYA', '58', '49-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('34', 'BUNYARUGURU', '84', '112-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('35', 'BUNYOLE EAST', '15', '60-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('36', 'BUNYOLE WEST', '15', '60-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('37', 'BURULI', '57', '25-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('38', 'BUSHENYI -ISHAKA MUNICIPALITY', '13', '4-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('39', 'BUSIA MUNICIPALITY', '14', '42-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('40', 'BUSIKI', '72', '75-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('41', 'BUSIRO', '92', '52-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('42', 'BUSUJJU', '62', '68-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('43', 'BUTAMBALA', '16', '99-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('44', 'BUTEBO', '82', '35-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('45', 'BUTEMBE', '25', '8-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('46', 'BUVUMA ISLANDS', '17', '90-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('47', 'BUWEKULA', '66', '31-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('48', 'BUYAGA EAST', '33', '16-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('49', 'BUYAGA WEST', '33', '16-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('50', 'BUYANJA', '33', '16-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('51', 'BUZAAYA', '29', '13-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('52', 'BWAMBA', '12', '3-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('53', 'CHEKWII', '68', '56-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('54', 'CHUA', '39', '19-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('55', 'DODOTH EAST', '26', '63-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('56', 'DODOTH WEST', '26', '63-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('57', 'DOKOLO', '19', '74-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('58', 'ENTEBBE MUNICIPALITY', '92', '52-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('59', 'ERUTE', '49', '22-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('60', 'GOMBA', '20', '91-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('61', 'GULU MUNICIPALITY', '21', '5-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('62', 'IBANDA', '22', '61-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('63', 'IGANGA MUNICIPALITY', '23', '7-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('64', 'IGARA', '13', '4-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('65', 'ISINGIRO', '24', '62-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('66', 'JIE', '42', '20-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('67', 'JINJA MUNICIPALITY', '25', '8-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('68', 'JONAM', '74', '33-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('69', 'KABULA', '53', '80-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('70', 'KAGOMA', '25', '8-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('71', 'KAJARA', '77', '34-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('72', 'KAKUUTO', '83', '36-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('73', 'KALUNGU', '28', '100-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('74', 'KAPELEBYONG', '1', '58-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('75', 'KASAMBYA', '66', '31-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('76', 'KASHARI', '60', '27-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('77', 'KASILO', '86', '97-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('78', 'KASSANDA', '66', '31-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('79', 'KATERERA', '84', '112-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('80', 'KATIKAMU', '51', '23-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('81', 'KAZO', '36', '65-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('82', 'KIBANDA', '37', '92-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('83', 'KIBOGA EAST', '34', '17-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('84', 'KIBOGA WEST', '45', '93-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('85', 'KIBUKU', '35', '102-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('86', 'KIGULU', '23', '7-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('87', 'KILAK', '2', '71-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('88', 'KOBOKO', '40', '66-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('89', 'KOLE', '41', '103-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('90', 'KONGASIS', '9', '59-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('91', 'KOOKI', '83', '36-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('92', 'KUMI', '43', '21-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('93', 'KWANIA', '3', '1-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('94', 'KWEEN', '44', '104-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('95', 'KYADONDO', '92', '52-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('96', 'KYAKA', '46', '84-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('97', 'KYOTERA', '83', '36-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('98', 'LAMWO', '48', '85-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('99', 'LIRA MUNICIPALITY', '49', '22-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('100', 'LUUKA', '50', '94-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('101', 'LWEMIYAGA', '90', '45-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('102', 'LWENGO', '52', '105-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('103', 'MANJIYA', '4', '78-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('104', 'MARACHA', '55', '77-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('105', 'MARUZI', '3', '1-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('106', 'MASAKA MUNICIPALITY', '56', '24-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('107', 'MASINDI MUNICIPALITY', '57', '25-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('108', 'MATHENIKO', '63', '28-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('109', 'MAWOGOLA', '90', '45-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('110', 'MAWOKOTA', '65', '30-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('111', 'MBALE MUNICIPALITY', '59', '26-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('112', 'MBARARA MUNICIPALITY', '60', '27-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('113', 'MITYANA', '62', '68-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('114', 'MOROTO MUNICIPALITY', '63', '28-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('115', 'MUKONO', '67', '32-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('116', 'MUKONO MUNICIPALITY', '67', '32-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('117', 'MWENGE', '47', '48-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('118', 'NAKASEKE NORTH', '69', '69-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('119', 'NAKASEKE SOUTH', '69', '69-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('120', 'NAKASONGOLA', '70', '44-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('121', 'NAKIFUMA', '67', '32-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('122', 'NGORA', '75', '108-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('123', 'NTENJERU', '32', '47-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('124', 'NTOROKO', '76', '96-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('125', 'NTUNGAMO MUNICIPALITY', '77', '34-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('126', 'NWOYA', '78', '110-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('127', 'NYABUSHOZI', '36', '65-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('128', 'OBONGI', '64', '29-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('129', 'OKORO', '94', '87-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('130', 'OMORO', '21', '5-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('131', 'OTUKE', '79', '86-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('132', 'OYAM', '80', '76-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('133', 'PADYERE', '74', '33-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('134', 'PALLISA', '82', '35-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('135', 'PIAN', '68', '56-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('136', 'RUBABO', '85', '37-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('137', 'RUHAAMA', '77', '34-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('138', 'RUHINDA', '61', '106-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('139', 'RUJUMBURA', '85', '37-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('140', 'RUKUNGIRI MUNICIPALITY', '85', '37-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('141', 'RUSHENYI', '77', '34-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('142', 'RWAMPARA', '60', '27-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('143', 'SAMIA BUGWE', '14', '42-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('144', 'SERERE', '86', '97-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('145', 'SHEEMA', '87', '101-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('146', 'SOROTI', '89', '38-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('147', 'SOROTI MUNICIPALITY', '89', '38-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('148', 'TINGEY', '30', '14-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('149', 'TOROMA', '31', '43-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('150', 'TORORO', '91', '39-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('151', 'TORORO MUNICIPALITY', '91', '39-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('152', 'USUK', '31', '43-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('153', 'WEST BUDAMA', '91', '39-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('154', 'WEST MOYO', '64', '29-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('155', 'CENTRAL', '95', '11301', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('156', 'KAWEMPE', '95', '11302', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('157', 'MAKINDYE', '95', '11303', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('158', 'NAKAWA', '95', '11304', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `county` VALUES ('159', 'RUBAGA', '95', '', '2014-10-08 13:09:40', '2014-10-08 13:09:40');

-- ----------------------------
-- Table structure for `course`
-- ----------------------------
DROP TABLE IF EXISTS `course`;
CREATE TABLE `course` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `code` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `courseType` int(11) DEFAULT NULL,
  `weight` int(11) DEFAULT NULL,
  `content` text,
  `subjectId` int(10) unsigned DEFAULT NULL,
  `status` int(11) NOT NULL,
  `timeFrame` int(11) DEFAULT NULL COMMENT 'in months',
  `courseCategoryId` int(11) unsigned NOT NULL,
  `courseCoreId` int(10) unsigned DEFAULT NULL,
  `campusId` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKCourseSubjects` (`subjectId`),
  KEY `subjectId` (`subjectId`),
  KEY `courseCategoryId` (`courseCategoryId`) USING BTREE,
  KEY `courseCampusId` (`campusId`) USING BTREE,
  KEY `courseCoreId` (`courseCoreId`) USING BTREE,
  CONSTRAINT `FKCourseCampus` FOREIGN KEY (`campusId`) REFERENCES `campus` (`id`),
  CONSTRAINT `FKCourseCategory` FOREIGN KEY (`courseCategoryId`) REFERENCES `course_category` (`id`),
  CONSTRAINT `FKCourseSubjects` FOREIGN KEY (`subjectId`) REFERENCES `subject` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `FKFFF43023593D2CEE` FOREIGN KEY (`subjectId`) REFERENCES `subject` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='All courses belonging to programs are stored in this table';

-- ----------------------------
-- Records of course
-- ----------------------------
INSERT INTO `course` VALUES ('1', null, 'Starting Your Own Business ', null, null, null, null, '0', null, '1', '0', '1');
INSERT INTO `course` VALUES ('2', null, 'Mobile Phone Repair', null, null, null, null, '0', null, '2', '2', '2');

-- ----------------------------
-- Table structure for `course_category`
-- ----------------------------
DROP TABLE IF EXISTS `course_category`;
CREATE TABLE `course_category` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of course_category
-- ----------------------------
INSERT INTO `course_category` VALUES ('1', 'Business');
INSERT INTO `course_category` VALUES ('2', 'Technology');
INSERT INTO `course_category` VALUES ('3', 'Leadership');
INSERT INTO `course_category` VALUES ('4', 'Life Skills');

-- ----------------------------
-- Table structure for `course_core`
-- ----------------------------
DROP TABLE IF EXISTS `course_core`;
CREATE TABLE `course_core` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `overview` text NOT NULL,
  `aims` text,
  `outcomes` text,
  `skills` text,
  `code` varchar(50) NOT NULL,
  `subjectId` int(11) unsigned DEFAULT NULL COMMENT 'The subject where this course belongs to, can be a humanity or anything',
  PRIMARY KEY (`id`),
  KEY `FKCourseSubject` (`subjectId`),
  KEY `subjectId` (`subjectId`),
  CONSTRAINT `FKCourseSubject` FOREIGN KEY (`subjectId`) REFERENCES `subject` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `FKD8B62AB9593D2CEE` FOREIGN KEY (`subjectId`) REFERENCES `subject` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='The core fundermental course elements, being referenced by a';

-- ----------------------------
-- Records of course_core
-- ----------------------------
INSERT INTO `course_core` VALUES ('1', 'Starting Your Own Business ', '<p>Any aspiring business person has to come to terms with basic concepts associated with a business. Throughout this course, we intend to discuss concepts like \'entrepreneurship\', \'entrepreneur\', \'enterprise\', \'the business idea\', and factors that influence your intention to start up a business.   \r\nWhile preparing the contents for this course, we took special measures to ensure that you would be able to relate to the different concepts introduced. In addition, we have kept the language as simple as possible to ensure clear understanding of the content. The activities that have been integrated throughout the units will enable you to gradually and systematically gather information and the right \'ingredients\' to be able to write up your business plan: the most important tool that will support your efforts to start up your own small business. </p>', null, null, null, '', null);
INSERT INTO `course_core` VALUES ('2', 'Mobile Phone Repair', '<p>Acquire skills in repairing commonly used electronics especially mobile phones</p>', null, null, null, '', null);

-- ----------------------------
-- Table structure for `district`
-- ----------------------------
DROP TABLE IF EXISTS `district`;
CREATE TABLE `district` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `code` varchar(50) DEFAULT NULL,
  `createdOn` datetime DEFAULT NULL,
  `lastModified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=latin1 COMMENT='Stores Districts in Uganda. Is used as a reference table.';

-- ----------------------------
-- Records of district
-- ----------------------------
INSERT INTO `district` VALUES ('1', 'AMURIA', '58', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('2', 'AMURU', '71', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('3', 'APAC', '1', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('4', 'BUDUDA', '78', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('5', 'BUGIRI', '41', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('6', 'BUHWEJU', '109', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('7', 'BUIKWE', '82', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('8', 'BUKOMANSIMBI', '98', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('9', 'BUKWO', '59', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('10', 'BULAMBULI', '89', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('11', 'BULIISA', '73', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('12', 'BUNDIBUGYO', '3', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('13', 'BUSHENYI', '4', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('14', 'BUSIA', '42', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('15', 'BUTALEJA', '60', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('16', 'BUTAMBALA', '99', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('17', 'BUVUMA', '90', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('18', 'BUYENDE', '83', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('19', 'DOKOLO', '74', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('20', 'GOMBA', '91', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('21', 'GULU', '5', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('22', 'IBANDA', '61', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('23', 'IGANGA', '7', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('24', 'ISINGIRO', '62', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('25', 'JINJA', '8', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('26', 'KAABONG', '63', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('27', 'KALIRO', '64', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('28', 'KALUNGU', '100', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('29', 'KAMULI', '13', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('30', 'KAPCHORWA', '14', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('31', 'KATAKWI', '43', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('32', 'KAYUNGA', '47', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('33', 'KIBAALE', '16', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('34', 'KIBOGA', '17', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('35', 'KIBUKU', '102', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('36', 'KIRUHURA', '65', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('37', 'KIRYANDONGO', '92', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('38', 'KISORO', '18', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('39', 'KITGUM', '19', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('40', 'KOBOKO', '66', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('41', 'KOLE', '103', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('42', 'KOTIDO', '20', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('43', 'KUMI', '21', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('44', 'KWEEN', '104', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('45', 'KYANKWANZI', '93', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('46', 'KYEGEGWA', '84', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('47', 'KYENJOJO', '48', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('48', 'LAMWO', '85', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('49', 'LIRA', '22', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('50', 'LUUKA', '94', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('51', 'LUWEERO', '23', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('52', 'LWENGO', '105', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('53', 'LYANTONDE', '80', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('54', 'MANAFWA', '67', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('55', 'MARACHA', '77', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('56', 'MASAKA', '24', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('57', 'MASINDI', '25', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('58', 'MAYUGE', '49', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('59', 'MBALE', '26', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('60', 'MBARARA', '27', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('61', 'MITOOMA', '106', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('62', 'MITYANA', '68', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('63', 'MOROTO', '28', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('64', 'MOYO', '29', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('65', 'MPIGI', '30', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('66', 'MUBENDE', '31', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('67', 'MUKONO', '32', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('68', 'NAKAPIRIPIRIT', '56', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('69', 'NAKASEKE', '69', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('70', 'NAKASONGOLA', '44', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('71', 'NAMAYINGO', '95', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('72', 'NAMUTUMBA', '75', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('73', 'NAPAK', '107', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('74', 'NEBBI', '33', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('75', 'NGORA', '108', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('76', 'NTOROKO', '96', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('77', 'NTUNGAMO', '34', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('78', 'NWOYA', '110', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('79', 'OTUKE', '86', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('80', 'OYAM', '76', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('81', 'PADER', '50', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('82', 'PALLISA', '35', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('83', 'RAKAI', '36', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('84', 'RUBIRIZI', '112', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('85', 'RUKUNGIRI', '37', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('86', 'SERERE', '97', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('87', 'SHEEMA', '101', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('88', 'SIRONKO', '51', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('89', 'SOROTI', '38', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('90', 'SSEMBABULE', '45', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('91', 'TORORO', '39', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('92', 'WAKISO', '52', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('93', 'YUMBE', '53', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('94', 'ZOMBO', '87', '2012-03-16 12:33:25', '2014-10-08 13:09:39');
INSERT INTO `district` VALUES ('95', 'KAMPALA', '113', '2014-06-13 11:13:45', '2014-10-08 13:09:39');

-- ----------------------------
-- Table structure for `general_information`
-- ----------------------------
DROP TABLE IF EXISTS `general_information`;
CREATE TABLE `general_information` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(250) NOT NULL,
  `content` text NOT NULL,
  `shortDescription` varchar(250) DEFAULT NULL,
  `createdOn` datetime NOT NULL,
  `defaultImageFilelName` varchar(250) DEFAULT NULL,
  `userId` int(10) unsigned DEFAULT NULL COMMENT 'The User Who entered this information',
  PRIMARY KEY (`id`),
  KEY `FKGeneralInformationUser` (`userId`),
  KEY `userId` (`userId`),
  CONSTRAINT `FKFEE4E9A065A1D7D8` FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  CONSTRAINT `FKGeneralInformationUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1 COMMENT='Stores General Information that is General regardless of the';

-- ----------------------------
-- Records of general_information
-- ----------------------------
INSERT INTO `general_information` VALUES ('1', 'Study With Us', '<p>WITU E-Learning platform&nbsp;offers a wide range of study opportunities at different levels of your careers. These range from undergraduate, graduate as well as continuing career enhancement options. If standard study options don\'t apply to you, flexible study options may be more suitable. These include Evening Study programmes designed to offer opportunities to mainly the working class although any interested person who meets the admission requirements can apply to be considered for admission.&nbsp;</p>\r\n<p>Ndejje University offers a wide range of study opportunities at different levels of your careers. These range from undergraduate, graduate as well as continuing career enhancement options. If standard study options don\'t apply to you, flexible study options may be more suitable. These include Evening Study programmes designed to offer opportunities to mainly the working class although any interested person who meets the admission requirements can apply to be considered for admission.&nbsp;</p>', '\r\nWITU E-Learning platform offers a wide range of study opportunities at different levels of your careers. These range from undergraduate, graduate as well a... ', '0001-01-01 00:00:00', 'efficient.png', null);
INSERT INTO `general_information` VALUES ('2', 'How To Apply', '<p>WITU E-Learning platform&nbsp;offers postgraduate courses at Masters and Diploma levels besides those offered at undergraduate levels. Prospective students should apply through the Academic Registrar to whom the applications should be sent before the beginning of every Semester (i.e. August or January) or every Session (i.e. August, December, April). Late applications are only accepted if the program space allows. These should be received not later than one month after the start of the appropriate semester; OR not later than a week after the beginning of a session. The admission procedure includes the Masters Program applicants.</p>\r\n<p>A prospective student can apply through the Ndejje University Online Self Service Admissions Management System by clicking the <span style=\"color: #999999;\"><a href=\"ApplyForm\"><span style=\"color: #999999;\">APPLY NOW&nbsp; </span></a></span>button and following through the prescribed steps guided by the Ndejje Online Applications Wizard. Paper - Based Application Forms can also be filled. The Form is obtainable from the Academic Registrar\'s Office(Main Campus) or at the Kampala Campus. Alternaltively, the<span style=\"color: #999999;\"> <a href=\"http://www.ndejjeuniversity.ac.ug/matt/download.php?f=undergrad.pdf\"><span style=\"color: #999999;\">Under Graduate </span></a></span>or&nbsp;<span style=\"color: #999999;\"><a href=\"http://www.ndejjeuniversity.ac.ug/matt/download.php?f=postgrad.pdf\"><span style=\"color: #999999;\">Post Graduate </span></a></span>forms can also be downloaded here.</p>\r\n<p>Application can also be made&nbsp; byfilling the application form obtainable from the Academic Registrar\'s office(Main Campus) or at the Kampala Campus. Further inquiries on specific issues may be sought by writing or telephoning directly to:&nbsp;</p>\r\n<p style=\"text-align: left; padding-left: 240px;\">The Academic Registrar Ndejje University<br />P. O. Box 7088, Kampala<br /><strong>Tel:</strong>&nbsp;+256-0392-730321<br /><strong>Fax:</strong>&nbsp;+256-312-202781<br /><strong>Email:</strong>&nbsp;<a href=\"mailto:registrar@ndejjeuniversity.ac.ug\">registrar@ndejjeuniversity.ac.ug&nbsp;</a></p>\r\n<p>All application forms must be accompanied by genuine copies of the \"0\" level certificates and/or pass slips; and for candidates who sat for \"A\" level before the current year, by a copy of the \"A\" -level certificate and/or pass slips.</p>\r\n<p>Four (4) recent passport size photographs, preferably colored, must be submitted with the application form.&nbsp;</p>\r\n<h5 style=\"color: #aa3734;\">Admission Requirements</h5>\r\n<p>Ndejje University has two principal avenues for entry into the University:</p>\r\n<h6 style=\"color: #1d268f;\">Direct \"A\" Level Entry</h6>\r\n<p>Candidates who have completed \"A\"- level are considered. They should have obtained two or more Principal passes at the same sitting at the \"A\" level examinations in any subject. Except when specifically provided in special faculty requirements, all subjects offered by the Uganda National Examinations Board are regarded as approved subjects for entry.&nbsp;</p>\r\n<h6 style=\"color: #1d268f;\">Mature Age Entry Scheme</h6>\r\n<p>(i) All candidates who may have attempted \"A\"-level and fail to get two principal passes but have relevant working experience, may seek admission to the University<br />(ii) Candidates who hold a Diploma with relevant qualifications (e.g. in education, business administration, etc) may seek admission. Candidates, who hold several certificates and have relevant experience, may also seek admission for the course relevant to their experience.</p>\r\n<p>Also, candidates who fail to get principal passes at \"A\"- level or international students who don\'t meet the minimum entry requirements can be admitted to the \"Advanced Certificate Course\" for one year. When they score an average of 60% in the final examinations, they may be admitted for the degree course in the following year.&nbsp;</p>', 'WITU E-Learning platform offers postgraduate courses at Masters and Diploma levels besides those offered at undergraduate levels. Prospective students should... ', '0001-01-01 00:00:00', 'intuitive.jpg', null);
INSERT INTO `general_information` VALUES ('3', 'Our Patners', '<p>All information pertaining to finances is obtained from the Bursar\'s office. The Bursar heads the Finance Department. His office is open from Monday through Friday, from 8.00 am to 5.00pm.&nbsp;</p>\r\n<h6 style=\"color: #1d268f;\">Payment of University Dues</h6>\r\n<div class=\"content\">Students are required to show proof of payment of all University dues to the Bursar\'s office. Fees for each Semester are payable at the beginning of that Semester, after which a student must register with the Academic Registrar\'s office; then he becomes a bona fide student of the University.<br />Cash payments to the Bursar are not accepted; money must be banked into one of the University Fees&nbsp;Collection Accounts.</div>\r\n<div class=\"content\">In addition to tuition fees, students are required to pay functional fees as shown below: </div>\r\n<div class=\"content\">\r\n<table style=\"height: 470px;\" width=\"520\" cellpadding=\"10\">\r\n<tbody>\r\n<tr>\r\n<td>&nbsp;</td>\r\n<td style=\"text-align: center;\"><strong>Semester I</strong></td>\r\n<td style=\"text-align: center;\"><strong>Semester II</strong></td>\r\n</tr>\r\n<tr>\r\n<td>Registration(Late&nbsp;Registraton&nbsp;50,000/=)</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Examination</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Library</td>\r\n<td style=\"text-align: right;\">150,000/=</td>\r\n<td style=\"text-align: right;\">150,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Development</td>\r\n<td style=\"text-align: right;\">120,000/=</td>\r\n<td style=\"text-align: right;\">120,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Sports&nbsp;Fee</td>\r\n<td style=\"text-align: right;\">25,000/=</td>\r\n<td style=\"text-align: right;\">25,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Guild</td>\r\n<td style=\"text-align: right;\">50,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Identity&nbsp;Card</td>\r\n<td style=\"text-align: right;\">20,&nbsp;000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>UNISA&nbsp;&amp;&nbsp;NCHE</td>\r\n<td style=\"text-align: right;\">21,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Medical&nbsp;Fees</td>\r\n<td style=\"text-align: right;\">50,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Chaplaincy&nbsp;Fee</td>\r\n<td style=\"text-align: right;\">10,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Bank&nbsp;Charges</td>\r\n<td style=\"text-align: right;\">2,500/=</td>\r\n<td style=\"text-align: right;\">2,500/=</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n</div>\r\n<h6 style=\"color: #1d268f;\">Accounts</h6>\r\n<div class=\"content\">(i) &nbsp; Centenary Rural Development Bank Entebbe Road Branch, Kampala<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>3010005055<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:</strong>&nbsp;Ndejje University Fees Collection Account&nbsp;<br />(ii) &nbsp;Centenary Rural Development Bank Wobulenzi Branch, Wobulenzi&nbsp;<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>3510021366<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:&nbsp;</strong>Ndejje University Fees Collection Account.<br />(iii) DFCU Bank, Uganda.<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>01013500075284<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:</strong>&nbsp;Ndejje University Fees Collection Account&nbsp;<br />(iv) Equity Bank, Uganda.<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong><span class=\"skype_c2c_print_container\">0200562412</span><br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:&nbsp;</strong>Ndejje University Fees Collection Account.</div>\r\n<div class=\"content\">Bank pay -in -slips to any of the above Accounts are obtainable from the Bursar\'s office or at any of the accounts offices on the three campuses. For DFCU and Equity Banks, banking slips are obtainable at the banks.</div>\r\n<h6 style=\"color: #1d268f;\">Cheque Payments</h6>\r\n<div class=\"content\">Personal cheques are not acceptable. Only verified Bankers\' Cheques (Bank Drafts) written in the names of \"Ndejje University\" are acceptable. The student is required to bring a copy of the Banking Pay -in -Slip to the Bursar\'s office: who will issue a receipt for the total amount banked.</div>\r\n<h6 style=\"color: #1d268f;\">Students\' Financial Records&nbsp;</h6>\r\n<div class=\"content\">The Bursar\'s office is ever ready to furnish each student with details regarding the payment of University dues. However, students are advised to keep copies of all Bank pay -in -slips and receipts issued by the Bursar\'s office.&nbsp;</div>\r\n<h6 class=\"content\" style=\"color: #1d268f;\">Contacts&nbsp;</h6>\r\n<div class=\"content\">The Bursar, Ndejje University<br />P. O. Box 7088, Kampala<br /><strong>Tel:</strong>&nbsp;<span class=\"skype_c2c_print_container\">+256-0392-730323</span><br /><strong>Fax:</strong>&nbsp;+256-312-202781<br /><strong>Email:</strong>&nbsp;bursar@ndejjeuniversity.ac.ug&nbsp;</div>\r\n<div id=\"skype_c2c_menu_container\" class=\"skype_c2c_menu_container\" style=\"display: none;\">\r\n<div class=\"skype_c2c_menu_click2call\"><a id=\"skype_c2c_menu_click2call_action\" class=\"skype_c2c_menu_click2call_action\"></a>Call</div>\r\n<div class=\"skype_c2c_menu_click2sms\"><a id=\"skype_c2c_menu_click2sms_action\" class=\"skype_c2c_menu_click2sms_action\"></a>Send SMS</div>\r\n<div class=\"skype_c2c_menu_add2skype\"><a id=\"skype_c2c_menu_add2skype_text\" class=\"skype_c2c_menu_add2skype_text\"></a>Add to Skype</div>\r\n<div class=\"skype_c2c_menu_toll_info\"><span class=\"skype_c2c_menu_toll_callcredit\">You\'ll need Skype Credit</span><span class=\"skype_c2c_menu_toll_free\">Free via Skype</span></div>\r\n</div>', 'All information pertaining to finances is obtained from the Bursar\'s office. The Bursar heads the Finance Department. His office is open from Monday t... ', '2014-10-08 13:09:12', 'reliable.jpg', null);
INSERT INTO `general_information` VALUES ('4', 'Test Detail', '<p>All information pertaining to finances is obtained from the Bursar\'s office. The Bursar heads the Finance Department. His office is open from Monday through Friday, from 8.00 am to 5.00pm.&nbsp;</p>\r\n<h6 style=\"color: #1d268f;\">Payment of University Dues</h6>\r\n<div class=\"content\">Students are required to show proof of payment of all University dues to the Bursar\'s office. Fees for each Semester are payable at the beginning of that Semester, after which a student must register with the Academic Registrar\'s office; then he becomes a bona fide student of the University.<br />Cash payments to the Bursar are not accepted; money must be banked into one of the University Fees&nbsp;Collection Accounts.</div>\r\n<div class=\"content\">In addition to tuition fees, students are required to pay functional fees as shown below: </div>\r\n<div class=\"content\">\r\n<table style=\"height: 470px;\" width=\"520\" cellpadding=\"10\">\r\n<tbody>\r\n<tr>\r\n<td>&nbsp;</td>\r\n<td style=\"text-align: center;\"><strong>Semester I</strong></td>\r\n<td style=\"text-align: center;\"><strong>Semester II</strong></td>\r\n</tr>\r\n<tr>\r\n<td>Registration(Late&nbsp;Registraton&nbsp;50,000/=)</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Examination</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n<td style=\"text-align: right;\">60,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Library</td>\r\n<td style=\"text-align: right;\">150,000/=</td>\r\n<td style=\"text-align: right;\">150,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Development</td>\r\n<td style=\"text-align: right;\">120,000/=</td>\r\n<td style=\"text-align: right;\">120,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Sports&nbsp;Fee</td>\r\n<td style=\"text-align: right;\">25,000/=</td>\r\n<td style=\"text-align: right;\">25,000/=</td>\r\n</tr>\r\n<tr>\r\n<td>Guild</td>\r\n<td style=\"text-align: right;\">50,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Identity&nbsp;Card</td>\r\n<td style=\"text-align: right;\">20,&nbsp;000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>UNISA&nbsp;&amp;&nbsp;NCHE</td>\r\n<td style=\"text-align: right;\">21,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Medical&nbsp;Fees</td>\r\n<td style=\"text-align: right;\">50,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Chaplaincy&nbsp;Fee</td>\r\n<td style=\"text-align: right;\">10,000/=</td>\r\n<td style=\"text-align: right;\">&nbsp;</td>\r\n</tr>\r\n<tr>\r\n<td>Bank&nbsp;Charges</td>\r\n<td style=\"text-align: right;\">2,500/=</td>\r\n<td style=\"text-align: right;\">2,500/=</td>\r\n</tr>\r\n</tbody>\r\n</table>\r\n</div>\r\n<h6 style=\"color: #1d268f;\">Accounts</h6>\r\n<div class=\"content\">(i) &nbsp; Centenary Rural Development Bank Entebbe Road Branch, Kampala<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>3010005055<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:</strong>&nbsp;Ndejje University Fees Collection Account&nbsp;<br />(ii) &nbsp;Centenary Rural Development Bank Wobulenzi Branch, Wobulenzi&nbsp;<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>3510021366<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:&nbsp;</strong>Ndejje University Fees Collection Account.<br />(iii) DFCU Bank, Uganda.<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong>01013500075284<br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:</strong>&nbsp;Ndejje University Fees Collection Account&nbsp;<br />(iv) Equity Bank, Uganda.<br /><strong>&nbsp; &nbsp; &nbsp; Account No:</strong><span class=\"skype_c2c_print_container\">0200562412</span><br /><strong>&nbsp; &nbsp; &nbsp; Title of Account:&nbsp;</strong>Ndejje University Fees Collection Account.</div>\r\n<div class=\"content\">Bank pay -in -slips to any of the above Accounts are obtainable from the Bursar\'s office or at any of the accounts offices on the three campuses. For DFCU and Equity Banks, banking slips are obtainable at the banks.</div>\r\n<h6 style=\"color: #1d268f;\">Cheque Payments</h6>\r\n<div class=\"content\">Personal cheques are not acceptable. Only verified Bankers\' Cheques (Bank Drafts) written in the names of \"Ndejje University\" are acceptable. The student is required to bring a copy of the Banking Pay -in -Slip to the Bursar\'s office: who will issue a receipt for the total amount banked.</div>\r\n<h6 style=\"color: #1d268f;\">Students\' Financial Records&nbsp;</h6>\r\n<div class=\"content\">The Bursar\'s office is ever ready to furnish each student with details regarding the payment of University dues. However, students are advised to keep copies of all Bank pay -in -slips and receipts issued by the Bursar\'s office.&nbsp;</div>\r\n<h6 class=\"content\" style=\"color: #1d268f;\">Contacts&nbsp;</h6>\r\n<div class=\"content\">The Bursar, Ndejje University<br />P. O. Box 7088, Kampala<br /><strong>Tel:</strong>&nbsp;<span class=\"skype_c2c_print_container\">+256-0392-730323</span><br /><strong>Fax:</strong>&nbsp;+256-312-202781<br /><strong>Email:</strong>&nbsp;bursar@ndejjeuniversity.ac.ug&nbsp;</div>\r\n<div id=\"skype_c2c_menu_container\" class=\"skype_c2c_menu_container\" style=\"display: none;\">\r\n<div class=\"skype_c2c_menu_click2call\"><a id=\"skype_c2c_menu_click2call_action\" class=\"skype_c2c_menu_click2call_action\"></a>Call</div>\r\n<div class=\"skype_c2c_menu_click2sms\"><a id=\"skype_c2c_menu_click2sms_action\" class=\"skype_c2c_menu_click2sms_action\"></a>Send SMS</div>\r\n<div class=\"skype_c2c_menu_add2skype\"><a id=\"skype_c2c_menu_add2skype_text\" class=\"skype_c2c_menu_add2skype_text\"></a>Add to Skype</div>\r\n<div class=\"skype_c2c_menu_toll_info\"><span class=\"skype_c2c_menu_toll_callcredit\">You\'ll need Skype Credit</span><span class=\"skype_c2c_menu_toll_free\">Free via Skype</span></div>\r\n', null, '2017-03-26 07:48:59', 'intuitive.jpg', null);

-- ----------------------------
-- Table structure for `general_information_attachment`
-- ----------------------------
DROP TABLE IF EXISTS `general_information_attachment`;
CREATE TABLE `general_information_attachment` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `generalInformationId` int(10) unsigned NOT NULL,
  `userFriendlyName` varchar(250) DEFAULT NULL COMMENT 'Name that will be displayed to the attachment.',
  `originalFileName` varchar(250) DEFAULT NULL,
  `fileName` varchar(250) NOT NULL COMMENT 'The Current File Name with which the file has been saved.',
  `fileType` varchar(50) NOT NULL,
  `description` text,
  `academicYearId` int(10) unsigned DEFAULT NULL COMMENT 'In case the Academic Year is required, then it can be attached.',
  PRIMARY KEY (`id`),
  KEY `FKGeneralInformationAttachmentGeneralInformation` (`generalInformationId`),
  KEY `FKGeneralInformationAttachmentAcademicYear` (`academicYearId`),
  KEY `generalInformationId` (`generalInformationId`),
  KEY `academicYearId` (`academicYearId`),
  CONSTRAINT `FKAB24B8993DAFC555` FOREIGN KEY (`generalInformationId`) REFERENCES `general_information` (`id`),
  CONSTRAINT `FKAB24B899F562EB67` FOREIGN KEY (`academicYearId`) REFERENCES `academic_year` (`id`),
  CONSTRAINT `FKGeneralInformationAttachmentAcademicYear` FOREIGN KEY (`academicYearId`) REFERENCES `academic_year` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `FKGeneralInformationAttachmentGeneralInformation` FOREIGN KEY (`generalInformationId`) REFERENCES `general_information` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Stores attachments added to general Information items.';

-- ----------------------------
-- Records of general_information_attachment
-- ----------------------------

-- ----------------------------
-- Table structure for `information_desk`
-- ----------------------------
DROP TABLE IF EXISTS `information_desk`;
CREATE TABLE `information_desk` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `type` int(11) NOT NULL COMMENT 'Can be 1 - News, 2 - Event or 3 - Notices',
  `title` varchar(150) NOT NULL,
  `content` text NOT NULL,
  `dateOfEvent` datetime DEFAULT NULL,
  `createdOn` datetime NOT NULL,
  `showStartDate` datetime DEFAULT NULL,
  `showEndDate` datetime DEFAULT NULL,
  `defaultImageFileName` varchar(150) DEFAULT NULL,
  `shortDescription` varchar(250) DEFAULT NULL,
  `userId` int(10) unsigned DEFAULT NULL COMMENT 'The User who entered the info',
  PRIMARY KEY (`id`),
  KEY `FKInformationDeskUser` (`userId`),
  KEY `userId` (`userId`),
  CONSTRAINT `FKB5DC0A4F65A1D7D8` FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  CONSTRAINT `FKInformationDeskUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1 COMMENT='Stores Notices, Events and news of the university';

-- ----------------------------
-- Records of information_desk
-- ----------------------------
INSERT INTO `information_desk` VALUES ('1', '0', 'Ndejje University ranked 2nd in Uganda and 42nd in Africa', '<p>Makerere University has been ranked the 18<sup>th</sup> best performing and reputable university among the 200 best universities in Africa, as per the new 4iCU.org university web rankings.</p>\r\n<p>However, this indicates that Makerere University has dropped by 14 positions from the third position, as ranked by Times Higher Education World University Rankings early last year.</p>\r\n<p>The University of Nairobi, in Kenya, which had been ranked 12<sup>th</sup> in the Times Higher Education of London in 2015, was ranked 14, thus becoming the best university in the East African Region. &nbsp;</p>\r\n<p>According to the rankings, Uganda&rsquo;s Ndejje University was ranked 42<sup>nd</sup>, Mbarara University of Science and Technology 73<sup>rd</sup> while Uganda Christian University was also positioned in the 91<sup>st</sup> position.</p>\r\n<p>This means that Ndejje becomes the Second Best University in the country and highly ranked private university in the country. South African Universities took the first 10 positions. They include; University of Cape Town in the 1<sup>st</sup> position, University of South Africa 2<sup>nd</sup>, Univeristy of Pretoria 3<sup>rd</sup>, &nbsp;&nbsp;Universities Stellenbosch 4<sup>th</sup>, University of the Witwatersrand 5<sup>th</sup>, North-West University 6<sup>th</sup>, University of KwaZulu-Natal 7<sup>th</sup>, Rhodes University 8<sup>th</sup>, University of Johannesburg 9<sup>th</sup> and University of the Western Cape ranked in the 10<sup>th</sup> position.</p>\r\n<p>Early this year, the Centre for World University Rankings (CWUR) ranked Makerere at the 846<sup>th</sup> position from 869<sup>th</sup> position in 2015, out of the 1,000 universities in the global rankings.</p>\r\n<p>CWRUR measures the quality of education and training of students as well as the prestige of the faculty members and the quality of their research without relying on surveys and university data submissions in addition to providing authoritative global university and provision of consulting services to governments and educational institutions who aspire to achieve world-class standards.</p>\r\n<p>According to the CWUR, Makerere ranks seventh on African continent and remains the best outside South Africa and Egypt. &nbsp;CWUR ranked the University of Witwatersrand from South Africa as Africa\'s finest at 176th position the world; University of Cape Town second at 265th; Stellenbosch University third at 329th; University of KwaZulu-Natal fourth at 468th; University of Pretoria fifth at 697th position while Egypt\'s Cairo University comes in at number six in Africa and 771st out of the top 1,000.</p>\r\n<p>Egypt\'s Ain Shams University at 960th; Mansoura University at 985th; and Alexandria University at 995th complete Africa\'s top ten and the continent\'s only universities in the top 1,000.</p>\r\n<p>A total of 224 Universities among the top 1,000 are from the US while 90 are from China, 74 from Japan, 65 from the United Kingdom, 56 from Germany, 48 from France, 48 from Italy, 41 from Spain, 36 from South Korea, 32 from Canada, 27 from Australia and Africa as a continent has only 10, South Africa dominating with 6, Egypt 3 and Uganda with only 1.</p>\r\n<p>In 2015, Thompson Reuter Corporation ranked Makerere in the ninth position, South Africa taking the first six universities with the University of Cape town in the first place, followed by &nbsp;the University of Witwatersrand, University of Stellenbosch taking the third, University of Pretoria the fourth, University of Kwazul-Natal the fifth, and lastly the University of Johannesburg. Egypt had the seventh and eighth rankings being the University of Cairo and Ain Shams University respectively.</p>', null, '0001-01-01 00:00:00', null, null, null, 'Makerere University has been ranked the 18th best performing and reputable university among the 200 best universities in Africa, as per the new 4iCU.org university web rankings.\r\nHowever, this indicat', null);
INSERT INTO `information_desk` VALUES ('2', '0', 'Mid Semester Tests - Semester 1 2016/17', '<p>Mid Semester Tests&nbsp; for Semester I 2016/17 will commence on Monday 19th September ending on Sunday 25th September 2016.</p>', null, '0001-01-01 00:00:00', null, null, null, 'Mid Semester Tests?á for Semester I 2016/17 will commence on Monday 19th September ending on Sunday 25th September 2016.', null);
INSERT INTO `information_desk` VALUES ('3', '0', 'Graduation Day', '<p>Graduation Day will be held on Friday, 14th October 2016</p>', null, '0001-01-01 00:00:00', null, null, null, 'Graduation Day will be held on Friday, 14th October 2016', null);
INSERT INTO `information_desk` VALUES ('4', '0', 'Ndejje University ranked 2nd in Uganda and 42nd in Africa ', '<p>Makerere ranked 18<sup>th</sup> in Africa, Ndejje 42<sup>nd</sup>. In the new rankings, Makerere comes after Cape Peninsula University of Technology from South Africa.<br /><br /> Makerere University has been ranked the 18<sup>th</sup> best performing and reputable university among the 200 best universities in Africa, as per the new <a href=\"http://l.facebook.com/l.php?u=http%3A%2F%2F4iCU.org%2F&amp;h=CAQE8MeeCAQHJ2-abOKWrmNy7Ksdk2xB-E8kOwsTMLIt1gQ&amp;enc=AZM0XNsyRdjtfu0zzAzraEgJwm93HfmjoW2tJmd_wlHTIvb4zTeNZColnoQUIZFBhBajUYZvF_usvYOFN2OQ2UW7uAjy5D9bp_hUZdDCStQGnycXttrkgzogkHNz3KuBnk1B_xPk0aEoGjmy4tYJRY6PwDH9Og8X2N92gBo01A1q7g&amp;s=1\" target=\"_blank\">4iCU.org</a> university web rankings. However, this indicates that Makerere University has dropped by 14 positions from the third position, as ranked by Times Higher Education World University Rankings early this year, last year. In the new rankings, Makerere comes after Cape Peninsula University of Technology from South Africa. According to the rankings, Uganda&rsquo;s Ndejje University was ranked 42<sup>nd</sup>, Mbarara University of Science and Technology 73<sup>rd</sup> while Uganda Christian University was also positioned in the 91<sup>st</sup> position. <br /><br /> This means that Ndejje becomes the Second Best University in the country and highly ranked private university in the country. South African Universities took the first 10 positions. They include; University of Cape Town in the 1st position, University of South Africa 2<sup>nd</sup>, Univeristy of Pretoria 3<sup>rd</sup>, Universities Stellenbosch 4<sup>th</sup>, University of the Witwatersrand 5<sup>th</sup>, North-West University 6<sup>th</sup>, University of KwaZulu-Natal 7<sup>th</sup>, Rhodes University 8<sup>th</sup>, University of Johannesburg 9th and University of the Western Cape ranked in the 10<sup>th</sup> position.<br /> <br /> Early this year, the Centre for World University Rankings (CWUR) ranked Makerere at the 846<sup>th</sup> position from 869<sup>th</sup> position in 2015, out of the 1,000 universities in the global rankings. CWRUR measures the quality of education and training of students as well as the prestige of the faculty members and the quality of their research without relying on surveys and university data submissions in addition to providing authoritative global university and provision of consulting services to governments and educational institutions who aspire to achieve world-class standards.<br /><br /> According to the CWUR, Makerere ranks seventh on African continent and remains the best outside South Africa and Egypt. CWUR ranked the University of Witwatersrand from South Africa as Africa\'s finest at 176<sup>th</sup> position the world; University of Cape Town second at 265<sup>th</sup>; Stellenbosch University third at 329<sup>th</sup>; University of KwaZulu-Natal fourth at 468<sup>th</sup>; University of Pretoria fifth at 697<sup>th</sup> position while Egypt\'s Cairo University comes in at number six in Africa and 771<sup>st</sup> out of the top 1,000.<br /> <br /> Egypt\'s Ain Shams University at 960<sup>th</sup>; Mansoura University at 985<sup>th</sup>; and Alexandria University at 995<sup>th</sup> complete Africa\'s top ten and the continent\'s only universities in the top 1,000. A total of 224 Universities among the top 1,000 are from the US while 90 are from China, 74 from Japan, 65 from the United Kingdom, 56 from Germany, 48 from France, 48 from Italy, 41 from Spain, 36 from South Korea, 32 from Canada, 27 from Australia and Africa as a continent has only 10, South Africa dominating with 6, Egypt 3 and Uganda with only 1. In 2015, Thompson Reuter Corporation ranked Makerere in the ninth position, South Africa taking the first six universities with the University of Cape town in the first place, followed by the University of Witwatersrand, University of Stellenbosch taking the third, University of Pretoria the fourth, University of Kwazul-Natal the fifth, and lastly the University of Johannesburg. Egypt had the seventh and eighth rankings being the University of Cairo and Ain Shams University respectively</p>', null, '0001-01-01 00:00:00', null, null, null, 'Makerere ranked 18th in Africa, Ndejje 42nd. In the new rankings, Makerere comes after Cape Peninsula University of Technology from South Africa. Makerere University has been ranked the 18th best perf', null);
INSERT INTO `information_desk` VALUES ('5', '0', 'Konstantinos and Marilena visit Ndejje University ', '<p>The duo from Greece on a one month visit to Uganda visited Ndejje University with numerous objectives. Guided by Fr. Antonios from Wobulenzi and Roland a student at Ndejje University the pair presented a series of important opportunities for the University, staff, students, and other university stake holders.</p>\r\n<p>Konstantinos and Marilena presented to the University staff, alumni, and students the many opportunities that lie in the future. The opportunities presented include scholarship, student and staff exchange program, networking, inter-university collaborations and partnerships, and shared academic programmes, among others. They introduced the Erasmus Mundus programme which aims to enhance the quality of higher education and promote dialogue and understanding between people and cultures through mobility and academic cooperation. The two have benefited from this and having been introduced to Africa it is a virgin recently opportunity that Ndejje University staff and students can exploit. <br /> <br /> In Greece about five Universities have shown interest to collaborate with Ndejje in the areas of Agriculture, Engineering, Business, Sports and Information Technology. This is as a result of the duo visiting Ndejje University and presenting the University&rsquo;s expression of interest to the five Universities. <br /> <br /> <em>&ldquo;Why export unskilled labour yet there is a chance for Education&rdquo;</em> said Fr. Antonios and this is the reason why he introduced Konstantinos and Marilena to Ndejje University.</p>', null, '0001-01-01 00:00:00', null, null, '20160810182127283_Konstantinos-and-Marilena-visit-Ndejje-University.png', 'The duo from Greece on a one month visit to Uganda visited Ndejje University with numerous objectives. Guided by Fr. Antonios from Wobulenzi and Roland a student at Ndejje University the pair presente', null);
INSERT INTO `information_desk` VALUES ('6', '0', 'End of Semester I 2016/17 Examinations', '<p>The End of Semester I 2016/17 Examinations shall start on Monday 21st November 2016 and end on Friday 2nd December 2016.</p>', null, '0001-01-01 00:00:00', null, null, null, 'The End of Semester I 2016/17 Examinations shall start on Monday 21st November 2016 and end on Friday 2nd December 2016.', null);
INSERT INTO `information_desk` VALUES ('7', '0', 'The MasterCard Foundation Scholars Program at UCT', '<p>The MasterCard Foundation has partnered with the University of Cape Town (UCT) to provide 300 scholarships ove<span class=\"text_exposed_show\">r 10 years to academically talented yet economically disadvantaged students from Sub-Saharan Africa for study at UCT. The Scholars Program is a $500 million initiative to educate young people &ndash; particularly from Africa &ndash; to lead change and make a positive social impact in their communities. Scholars will receive comprehensive scholarships, academic support, peer mentorship, career guidance, internship opportunities, transition-to-work support and access to a global alumni network. - See more at: <a href=\"https://web.facebook.com/l.php?u=http%3A%2F%2Fwww.mcfsp.uct.ac.za%2F%23sthash.xnsxTths.dpuf&amp;h=wAQEZ3JYnAQHUPg_8uUObqs4LsBHTse-hxaiGtA-GT-D9Sg&amp;enc=AZOyDddHgqsQKVeNZGYgtQNnOV8e7ziJdaRHmCYXgj7lRVt24CN3KR-pj5pmdP0JDSb8i82wr9speRSRKgwYpW7B3wJp-qHQPIT8Ia3sllaaKkKLU5DogF2LaJWCUTAO7PpDZuwNzzWJFpUaOIQP5oQylpRuyTbwxQ1y6ujfOg8eVFgickgFAxcPp1nWgaH3qvx9gqbzBA6h63JkuUcPRFB6&amp;s=1\" target=\"_blank\" rel=\"nofollow\">http://www.mcfsp.uct.ac.za/#sthash.xnsxTths.dpuf</a></span></p>', null, '0001-01-01 00:00:00', null, null, null, 'The MasterCard Foundation has partnered with the University of Cape Town (UCT) to provide 300 scholarships over 10 years to academically talented yet economically disadvantaged students from Sub-Sahar', null);
INSERT INTO `information_desk` VALUES ('8', '0', 'End of Semester II 2015/16, AR\'s Communication ', '<p><strong>Dear Students, Parents, Guardians and Staff:</strong> <br /><br /> Very warm, Christian greetings from the University Management and office of the Academic Registrar, Ndejje University. <br /><br /> Today, May 27, 2016 marks the end of Semester II, of 2015/16 Academic year. On behalf of the University, we congratulate you upon the successful completion of the Semester; your financial and material support is highly appreciated. We pray that the Almighty God rewards you abundantly! <br /><br /><strong>Please kindly do observe these very important days and events during the vacation and Semester I of Academic year 2016/2017.</strong></p>\r\n<ol>\r\n<li>Semester I of 2016/2017 will commence for weekend programme on <strong>Saturday, 6<sup>th</sup> August 2016 and Monday 8<sup>th</sup> August 2016</strong> for the day/evening programmes. Lectures will begin promptly and registration for all students will begin immediately. <br /><br /> The students studying at the Main Campus, but residing in Halls of Residence, should report either on Saturday, 6/08/2016 <strong>OR</strong> Sunday, 7/08/2016.</li>\r\n<li>All students are required to have paid 100% before reporting for the Semester. In the unlikely event that a student fails to pay 100%, he/she must pay 60% or more at the beginning of the Semester. Please, take note, <strong>no lectures</strong> will be attended by students without the 60% payment at the beginning of the Semester. For the borders, no student will be allowed to access any services before payment of 60%.</li>\r\n<li>You are reminded that Dead Year/Semester should always be applied for within the first two (2) weeks after commencement of the Semester.</li>\r\n<li>A student is required to attend the minimum 75% of the fifteen (15) lecture weeks in a Semester to be eligible for examinations, then 2 weeks are for exams; and all course works <strong>must</strong> be done and handed in two (2) weeks before the Semester&rsquo;s examinations.</li>\r\n<li>The students should adhere to the values of the University: please read the Ndejje University Norms and Regulations booklet.</li>\r\n<li>It is mandatory for all 2nd year students to do Internship/Fieldwork/School Practice during the June-July holiday; therefore, you are requested to search early for placement in organizations and register with the Coordinator&rsquo;s office. You will also be required to pay a little extra fee; <strong>the amount depending on the set requirements of your Faculty for the exercise</strong> and make sure you are allocated a Supervisor, before you leave for holidays.</li>\r\n<li>All the continuing students are kindly requested to note also the few changes in the existing fees structure as <strong>a result of the inevitable inflationary tendencies in the economy.</strong> </li>\r\n<li>All essential offices and services such as the Vice Chancellor, Deputy Vice Chancellor, University Secretary, Academic Registrar, Deputy Academic Registrar, Dean of Students&rsquo; office, Faculty Deans, Accounts, Administrative Assistants, Professional Courses&rsquo; Coordinator, Library and a few others will remain functional during the vacation.</li>\r\n<li>This communication should be regarded to be both official and very important; please do disregard all other versions regarding changes in the University fees.</li>\r\n</ol>\r\n<p>Should you have any inquiries, please ring telephones: <strong>0392 730321 / 0752 730334</strong> OR email: <a href=\"mailto:registrar@ndejjeuniversity.ac.ug\">registrar@ndejjeuniversity.ac.ug</a> OR <a href=\"mailto:dar@ndejjeuniversity.ac.ug\">dar@ndejjeuniversity.ac.ug</a> <br /><br /> We wish you and your families a very fruitful vacation that will enable you to report back promptly on either Saturday, 6th , Sunday, 7th (Kampala Campus and Boarders at Main Campus) OR Monday 8th August, 2016 ready for the registration and lectures. Lectures will start promptly. Adios! <br /><br /> Yours sincerely, <br /><br /> (B. M. Sekabembe)<br /> <strong>ACADEMIC REGISTRAR</strong><br /><strong> for and on behalf of Management</strong></p>', null, '0001-01-01 00:00:00', null, null, null, 'Dear Students, Parents, Guardians and Staff: Very warm, Christian greetings from the University Management and office of the Academic Registrar, Ndejje University.  Today, May 27, 2016 marks the end o', null);

-- ----------------------------
-- Table structure for `institution`
-- ----------------------------
DROP TABLE IF EXISTS `institution`;
CREATE TABLE `institution` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(250) NOT NULL,
  `shortName` varchar(100) DEFAULT NULL,
  `logoPathName` varchar(150) DEFAULT NULL COMMENT 'File name of the logo of the university, the path shall NOT be stored, instead a standard path shall be used',
  `description` text COMMENT 'This is more of a short summary of the university.',
  `createdOn` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='Stores the basic information about the university';

-- ----------------------------
-- Records of institution
-- ----------------------------
INSERT INTO `institution` VALUES ('1', 'Women In Technology', 'WITU', null, null, null);

-- ----------------------------
-- Table structure for `instructor`
-- ----------------------------
DROP TABLE IF EXISTS `instructor`;
CREATE TABLE `instructor` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `personId` int(10) unsigned DEFAULT NULL,
  `about` text,
  `researchInterests` text,
  `userId` int(10) unsigned DEFAULT NULL,
  `createdOn` datetime DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKStaffPerson` (`personId`),
  KEY `FKStaffUser` (`userId`),
  KEY `personId` (`personId`),
  KEY `userId` (`userId`),
  CONSTRAINT `FKBD79A94A65A1D7D8` FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  CONSTRAINT `FKStaffUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='Stores Everything about the staff ';

-- ----------------------------
-- Records of instructor
-- ----------------------------
INSERT INTO `instructor` VALUES ('1', '1', 'Instructor', null, '2', '2017-04-15 10:12:34', 'admin');

-- ----------------------------
-- Table structure for `instructor_position`
-- ----------------------------
DROP TABLE IF EXISTS `instructor_position`;
CREATE TABLE `instructor_position` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `positionId` int(10) unsigned NOT NULL,
  `assignedDate` date NOT NULL,
  `deactivatedDate` date DEFAULT NULL,
  `idNumber` varchar(50) DEFAULT NULL,
  `isActive` tinyint(4) DEFAULT NULL,
  `workStatus` int(11) DEFAULT NULL,
  `academicQaulification` varchar(250) DEFAULT NULL,
  `instructorId` int(10) unsigned NOT NULL,
  `levelId` int(10) unsigned NOT NULL COMMENT 'The position corresponding to the level Id that was selected in the database, e.g if Faculty of Engineering was selected, then its id shall be in this position. This works inhand with the level of the position(foreign key)',
  `campusId` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `campusId` (`campusId`),
  KEY `positionId` (`positionId`),
  KEY `staffId` (`instructorId`),
  KEY `campusId_2` (`campusId`),
  KEY `FKInstructorPositionPosition` (`positionId`) USING BTREE,
  KEY `FKInstructorPositionInstructor` (`instructorId`) USING BTREE,
  CONSTRAINT `FK9543C61949F47030` FOREIGN KEY (`instructorId`) REFERENCES `staff` (`id`),
  CONSTRAINT `FK9543C619A647E5EB` FOREIGN KEY (`campusId`) REFERENCES `campus` (`id`),
  CONSTRAINT `FK9543C619EB197438` FOREIGN KEY (`positionId`) REFERENCES `position` (`id`),
  CONSTRAINT `FKStaffPositionCampus` FOREIGN KEY (`campusId`) REFERENCES `campus` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FKStaffPositionPosition` FOREIGN KEY (`positionId`) REFERENCES `position` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FKStaffPositionStaff` FOREIGN KEY (`instructorId`) REFERENCES `staff` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='Stores positions held by staff, corresponding to the positio';

-- ----------------------------
-- Records of instructor_position
-- ----------------------------
INSERT INTO `instructor_position` VALUES ('1', '1', '2016-07-20', null, '-', '0', '0', null, '225', '3', '9');
INSERT INTO `instructor_position` VALUES ('2', '2', '2016-08-10', null, '-', '1', '0', null, '187', '3', '9');
INSERT INTO `instructor_position` VALUES ('3', '3', '2016-08-10', null, '-', '1', '0', null, '230', '3', '9');

-- ----------------------------
-- Table structure for `person`
-- ----------------------------
DROP TABLE IF EXISTS `person`;
CREATE TABLE `person` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `surname` varchar(150) NOT NULL,
  `givenName` varchar(150) NOT NULL,
  `otherName` varchar(150) DEFAULT NULL,
  `gender` tinyint(4) DEFAULT NULL COMMENT 'Options: 1 - Male, 2 - Female',
  `dateOfBirth` date DEFAULT NULL,
  `title` varchar(50) DEFAULT NULL,
  `maritalStatus` varchar(50) DEFAULT NULL,
  `religion` varchar(50) DEFAULT NULL,
  `nationalityId` int(10) unsigned DEFAULT NULL,
  `countryOfResidenceId` int(10) unsigned DEFAULT NULL,
  `countryOfBirthId` int(10) unsigned DEFAULT NULL,
  `districtOfBirthId` int(10) unsigned DEFAULT NULL,
  `countyOfBirthId` int(10) unsigned DEFAULT NULL,
  `subCountyOfBirthId` int(10) unsigned DEFAULT NULL,
  `districtOfResidenceId` int(10) unsigned DEFAULT NULL,
  `countyOfResidenceId` int(10) unsigned DEFAULT NULL,
  `subCountyOfResidenceId` int(10) unsigned DEFAULT NULL,
  `placeOfBirth` varchar(250) DEFAULT NULL,
  `postalAddress` varchar(250) DEFAULT NULL,
  `permentAddress` varchar(250) DEFAULT NULL,
  `preferredLanguage` varchar(100) DEFAULT NULL,
  `homeLanguage` varchar(100) DEFAULT NULL,
  `occupation` varchar(100) DEFAULT NULL,
  `emailAddress` varchar(100) DEFAULT NULL,
  `altEmailAddress` varchar(100) DEFAULT NULL,
  `telephoneContact` varchar(100) DEFAULT NULL,
  `altTelephoneContact` varchar(100) DEFAULT NULL,
  `nextOfKinName` varchar(250) DEFAULT NULL,
  `nextOfKinRelationship` varchar(100) DEFAULT NULL,
  `nextOfKinAddress` varchar(100) DEFAULT NULL,
  `nextOfKinContact` varchar(100) DEFAULT NULL,
  `organisation` varchar(100) DEFAULT NULL,
  `websiteUrl` varchar(100) DEFAULT NULL,
  `profilePhotoName` varchar(100) DEFAULT NULL,
  `personOwnerType` int(11) DEFAULT NULL COMMENT 'Options: 1 - Staff, 2 - Student, 3 - Prospective Student',
  `createdOn` datetime DEFAULT NULL,
  `lastModified` datetime DEFAULT NULL,
  `person_username_registrationnumber` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKPersonNationality` (`nationalityId`),
  KEY `FKPersonCountryResidence` (`countryOfResidenceId`),
  KEY `FKPersonCountryBirth` (`countryOfBirthId`),
  KEY `FKPersonDistrictBirth` (`districtOfBirthId`),
  KEY `FKPersonCountyBirth` (`countyOfBirthId`),
  KEY `FKPErsonSubCountyBirth` (`subCountyOfBirthId`),
  KEY `FKPersonDistrictResidence` (`districtOfResidenceId`),
  KEY `FKPersonCountyResidence` (`countyOfResidenceId`),
  KEY `FKPersonSubCountyResidence` (`subCountyOfResidenceId`),
  KEY `countryOfBirthId` (`countryOfBirthId`),
  KEY `countryOfResidenceId` (`countryOfResidenceId`),
  KEY `nationalityId` (`nationalityId`),
  KEY `countyOfBirthId` (`countyOfBirthId`),
  KEY `countyOfResidenceId` (`countyOfResidenceId`),
  KEY `districtOfBirthId` (`districtOfBirthId`),
  KEY `districtOfResidenceId` (`districtOfResidenceId`),
  KEY `subCountyOfBirthId` (`subCountyOfBirthId`),
  KEY `subCountyOfResidenceId` (`subCountyOfResidenceId`),
  CONSTRAINT `person_ibfk_1` FOREIGN KEY (`countyOfBirthId`) REFERENCES `county` (`id`),
  CONSTRAINT `person_ibfk_10` FOREIGN KEY (`countryOfBirthId`) REFERENCES `country` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_11` FOREIGN KEY (`countryOfResidenceId`) REFERENCES `country` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_12` FOREIGN KEY (`countyOfBirthId`) REFERENCES `county` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_13` FOREIGN KEY (`countyOfResidenceId`) REFERENCES `county` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_14` FOREIGN KEY (`districtOfBirthId`) REFERENCES `district` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_15` FOREIGN KEY (`districtOfResidenceId`) REFERENCES `district` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_16` FOREIGN KEY (`nationalityId`) REFERENCES `country` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_17` FOREIGN KEY (`subCountyOfBirthId`) REFERENCES `sub_county` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_18` FOREIGN KEY (`subCountyOfResidenceId`) REFERENCES `sub_county` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION,
  CONSTRAINT `person_ibfk_2` FOREIGN KEY (`countyOfResidenceId`) REFERENCES `county` (`id`),
  CONSTRAINT `person_ibfk_3` FOREIGN KEY (`districtOfResidenceId`) REFERENCES `district` (`id`),
  CONSTRAINT `person_ibfk_4` FOREIGN KEY (`countryOfResidenceId`) REFERENCES `country` (`id`),
  CONSTRAINT `person_ibfk_5` FOREIGN KEY (`subCountyOfBirthId`) REFERENCES `sub_county` (`id`),
  CONSTRAINT `person_ibfk_6` FOREIGN KEY (`countryOfBirthId`) REFERENCES `country` (`id`),
  CONSTRAINT `person_ibfk_7` FOREIGN KEY (`nationalityId`) REFERENCES `country` (`id`),
  CONSTRAINT `person_ibfk_8` FOREIGN KEY (`districtOfBirthId`) REFERENCES `district` (`id`),
  CONSTRAINT `person_ibfk_9` FOREIGN KEY (`subCountyOfResidenceId`) REFERENCES `sub_county` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='Stores all the people that are accessing the application. Th';

-- ----------------------------
-- Records of person
-- ----------------------------
INSERT INTO `person` VALUES ('1', 'Ahabwe', '', 'Elizabeth', '2', '1990-07-27', 'Mrs', '2', null, '1', '1', '1', '1', '1', '1', '1', '1', '1', 'Kabaale', null, null, null, null, null, null, null, '07745156161', null, 'Stuart', 'Husband', null, '024515616', null, null, null, null, null, null, null);

-- ----------------------------
-- Table structure for `position`
-- ----------------------------
DROP TABLE IF EXISTS `position`;
CREATE TABLE `position` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `level` varchar(100) NOT NULL COMMENT 'Administration, Campus ',
  `positionCategoryId` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKPositonpositionCategory` (`positionCategoryId`),
  KEY `positionCategoryId` (`positionCategoryId`),
  CONSTRAINT `FKFD95C672A1E26F61` FOREIGN KEY (`positionCategoryId`) REFERENCES `position_category` (`id`),
  CONSTRAINT `FKPositonpositionCategory` FOREIGN KEY (`positionCategoryId`) REFERENCES `position_category` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='Lists all types of positions that a university has.';

-- ----------------------------
-- Records of position
-- ----------------------------
INSERT INTO `position` VALUES ('1', 'Vice Chancellor', 'University', '1');
INSERT INTO `position` VALUES ('2', 'Deputy Vice Chancellor', 'University', '2');
INSERT INTO `position` VALUES ('3', 'University Secretary', 'University', '3');

-- ----------------------------
-- Table structure for `position_category`
-- ----------------------------
DROP TABLE IF EXISTS `position_category`;
CREATE TABLE `position_category` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of position_category
-- ----------------------------
INSERT INTO `position_category` VALUES ('1', 'Systems Administrator');
INSERT INTO `position_category` VALUES ('2', 'Management');
INSERT INTO `position_category` VALUES ('3', 'Personal Assistant');

-- ----------------------------
-- Table structure for `role`
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `serviceId` int(10) unsigned DEFAULT NULL,
  `type` int(10) unsigned DEFAULT NULL COMMENT '1 - View, 2 - Add Or Edit, 3 - Delete',
  `requiresAccessScope` tinyint(4) NOT NULL,
  `canCreate` tinyint(4) DEFAULT NULL,
  `canRead` tinyint(4) DEFAULT NULL,
  `canUpdate` tinyint(4) DEFAULT NULL,
  `canDelete` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKRoleService` (`serviceId`),
  KEY `serviceId` (`serviceId`),
  CONSTRAINT `FK6117A7ECD9F2CC1D` FOREIGN KEY (`serviceId`) REFERENCES `service` (`id`),
  CONSTRAINT `FKRoleService` FOREIGN KEY (`serviceId`) REFERENCES `service` (`id`) ON DELETE SET NULL ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Stores all the roles the applicaiton uses for validaiton.';

-- ----------------------------
-- Records of role
-- ----------------------------

-- ----------------------------
-- Table structure for `service`
-- ----------------------------
DROP TABLE IF EXISTS `service`;
CREATE TABLE `service` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `category` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Stores all the services from which the roles are created. ';

-- ----------------------------
-- Records of service
-- ----------------------------

-- ----------------------------
-- Table structure for `subject`
-- ----------------------------
DROP TABLE IF EXISTS `subject`;
CREATE TABLE `subject` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Stores the types of courses that group courses, such as huma';

-- ----------------------------
-- Records of subject
-- ----------------------------

-- ----------------------------
-- Table structure for `sub_county`
-- ----------------------------
DROP TABLE IF EXISTS `sub_county`;
CREATE TABLE `sub_county` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `countyId` int(10) unsigned NOT NULL,
  `code` varchar(50) DEFAULT NULL,
  `createdOn` datetime DEFAULT NULL,
  `lastModified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FKSubCountyCounty` (`countyId`),
  KEY `countyId` (`countyId`),
  CONSTRAINT `FK4580E74942276459` FOREIGN KEY (`countyId`) REFERENCES `county` (`id`),
  CONSTRAINT `FKSubCountyCounty` FOREIGN KEY (`countyId`) REFERENCES `county` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1060 DEFAULT CHARSET=latin1 COMMENT='SubCounties in Uganda. This table will be used as a referenc';

-- ----------------------------
-- Records of sub_county
-- ----------------------------
INSERT INTO `sub_county` VALUES ('1', 'ABANGA', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('2', 'ABARILELA', '2', '58-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('3', 'ABER', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('4', 'ABOK', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('5', 'ABOKE', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('6', 'ABONGOMOLA', '93', '1-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('7', 'ABUKU', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('8', 'ACHABA', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('9', 'ACHOLI-BUR', '4', '50-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('10', 'ACOWA', '74', '58-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('11', 'ADEKNINO', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('12', 'ADEKOKWOK', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('13', 'ADOK', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('14', 'ADUKU', '93', '1-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('15', 'ADUKU TOWN COUNCIL', '93', '1-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('16', 'ADWARI', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('17', 'ADYEL', '99', '22-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('18', 'AGALI', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('19', 'AGORO', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('20', 'AGULE', '1', '35-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('21', 'AGWATTA', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('22', 'AGWENG', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('23', 'AKALO', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('24', 'AKISIM', '1', '35-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('25', 'AKOKORO', '105', '1-02-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('26', 'AKWANG', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('27', 'AKWORO', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('28', 'ALEKA', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('29', 'ALERO', '126', '110-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('30', 'ALIBA', '128', '29-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('31', 'ALITO', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('32', 'ALWI', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('33', 'AMACH', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('34', 'AMURIA TOWN COUNCIL', '2', '58-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('35', 'AMURU', '87', '71-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('36', 'AMURU TOWN COUNCIL', '87', '71-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('37', 'AMWOMA', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('38', 'ANAKA (PAYIRA)', '126', '110-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('39', 'ANAKA TOWN COUNCIL', '126', '110-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('40', 'ANGAGURA', '4', '50-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('41', 'APAC', '105', '1-02-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('42', 'APAC TOWN COUNCIL', '105', '1-02-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('43', 'APO', '3', '53-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('44', 'APOPONG', '1', '35-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('45', 'ARAPAI', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('46', 'ARIWA', '3', '53-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('47', 'AROMO', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('48', 'ASAMUK', '2', '58-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('49', 'ASURET', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('50', 'ATANGA', '4', '50-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('51', 'ATEGO', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('52', 'ATIAK', '87', '71-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('53', 'ATIIRA', '144', '97-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('54', 'ATUTUR', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('55', 'ATYAK', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('56', 'AWACH', '5', '5-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('57', 'AWERE', '4', '50-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('58', 'AYER', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('59', 'BAGEZZA', '75', '31-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('60', 'BAITAMBOGWE', '33', '49-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('61', 'BALAWOLI', '15', '13-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('62', 'BALLA', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('63', 'BAMUNANIKA', '6', '23-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('64', 'BANDA', '27', '95-02-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('65', 'BAR-DEGE', '61', '5-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('66', 'BARR', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('67', 'BATTA', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('68', 'BBAALE', '7', '47-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('69', 'BBANDA', '42', '68-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('70', 'BENET', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('71', 'BIGASA', '24', '98-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('72', 'BIHANGA', '20', '109-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('73', 'BIHARWE', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('74', 'BIISO', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('75', 'BINYINY', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('76', 'BINYINYI TOWN COUNCIL', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('77', 'BIREMBO', '17', '16-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('78', 'BIRERE', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('79', 'BISHESHE', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('80', 'BITEREKO', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('81', 'BITOOMA', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('82', 'BITSYA', '20', '109-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('83', 'BOBI', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('84', 'BOMBO TOWN COUNCIL', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('85', 'BUBAARE', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('86', 'BUBANDI', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('87', 'BUBANGO', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('88', 'BUBIITA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('89', 'BUBUKWANGA', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('90', 'BUBUTU', '9', '67-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('91', 'BUBYANGU', '32', '26-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('92', 'BUDADIRI TOWN COUNCIL', '10', '51-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('93', 'BUDDE', '43', '99-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('94', 'BUDHAYA', '25', '41-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('95', 'BUDONDO', '70', '8-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('96', 'BUDONGO', '22', '25-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('97', 'BUDUDA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('98', 'BUDUDA TOWN COUNCIL', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('99', 'BUDUMBA', '36', '60-02-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('100', 'BUDWALE', '32', '26-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('101', 'BUFUMBO', '32', '26-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('102', 'BUFUNJO', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('103', 'BUGAAKI', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('104', 'BUGAMBA', '142', '27-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('105', 'BUGANGARI', '139', '37-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('106', 'BUGAYA', '11', '83-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('107', 'BUGEMBE TOWN COUNCIL', '45', '8-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('108', 'BUGINYANYA', '29', '89-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('109', 'BUGIRI TOWN COUNCIL', '25', '41-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('110', 'BUGITIMWA', '10', '51-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('111', 'BUGOBERO', '9', '67-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('112', 'BUGONDO', '77', '97-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('113', 'BUGONGI', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('114', 'BUGONGI TOWN COUNCIL', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('115', 'BUGULUMBYA', '51', '13-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('116', 'BUHEHE', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('117', 'BUHEMBA', '27', '95-02-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('118', 'BUHUGU', '10', '51-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('119', 'BUHUNGA', '139', '37-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('120', 'BUIKWE', '21', '82-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('121', 'BUIKWE TOWN COUNCIL', '21', '82-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('122', 'BUKABOOLI', '33', '49-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('123', 'BUKAKATA', '28', '24-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('124', 'BUKALASI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('125', 'BUKANGA', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('126', 'BUKASAKYA', '32', '26-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('127', 'BUKATUBE', '33', '49-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('128', 'BUKHABUSI', '9', '67-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('129', 'BUKHALU', '29', '89-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('130', 'BUKHAWEKA', '9', '67-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('131', 'BUKHIENDE', '32', '26-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('132', 'BUKHOFU', '9', '67-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('133', 'BUKHULO', '10', '51-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('134', 'BUKIABI', '9', '67-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('135', 'BUKIBOKOLO', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('136', 'BUKIGAI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('137', 'BUKIIRO', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('138', 'BUKIISE', '10', '51-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('139', 'BUKIMBIRI', '14', '18-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('140', 'BUKIYI', '10', '51-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('141', 'BUKOHO', '9', '67-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('142', 'BUKOMANSIMBI TOWN COUNCIL', '24', '98-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('143', 'BUKOMERO', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('144', 'BUKONDE', '32', '26-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('145', 'BUKONZO', '18', '3-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('146', 'BUKOOMA', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('147', 'BUKULULA', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('148', 'BUKUSU', '9', '67-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('149', 'BUKUYA', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('150', 'BUKWA', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('151', 'BUKWO TOWN COUNCIL', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('152', 'BUKYABO', '10', '51-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('153', 'BUKYAMBI', '10', '51-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('154', 'BULAGO', '29', '89-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('155', 'BULAMAGI', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('156', 'BULANGE', '40', '75-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('157', 'BULANGIRA', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('158', 'BULEGENI', '29', '89-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('159', 'BULEGENI TOWN COUNCIL', '29', '89-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('160', 'BULERA', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('161', 'BULESA', '25', '41-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('162', 'BULIDHA', '25', '41-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('163', 'BULIISA', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('164', 'BULIISA TOWN COUNCIL', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('165', 'BULO', '43', '99-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('166', 'BULONGO', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('167', 'BULOPA', '15', '13-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('168', 'BULUCHEKE', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('169', 'BULUGANYA', '29', '89-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('170', 'BULUGUYI', '25', '41-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('171', 'BULUMBI', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('172', 'BUMALIMBA', '10', '51-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('173', 'BUMANYA', '30', '64-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('174', 'BUMASHETI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('175', 'BUMASIFWA', '10', '51-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('176', 'BUMASIKYE', '32', '26-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('177', 'BUMASOBO', '29', '89-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('178', 'BUMAYOKA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('179', 'BUMBAIRE', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('180', 'BUMBO', '9', '67-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('181', 'BUMBOBI', '32', '26-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('182', 'BUMWONI', '9', '67-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('183', 'BUNABWANA', '9', '67-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('184', 'BUNAMBUTYE', '29', '89-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('185', 'BUNDIBUGYO TOWN COUNCIL', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('186', 'BUNGATIRA', '5', '5-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('187', 'BUNGOKHO', '32', '26-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('188', 'BUNYAFWA', '10', '51-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('189', 'BUPOTO', '9', '67-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('190', 'BUREMBA', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('191', 'BURERE', '20', '109-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('192', 'BURORA', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('193', 'BURUNGA', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('194', 'BUSAANA', '123', '47-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('195', 'BUSABA', '36', '60-02-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('196', 'BUSABI', '36', '60-02-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('197', 'BUSAKIRA', '33', '49-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('198', 'BUSAMUZI', '46', '90-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('199', 'BUSANO', '32', '26-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('200', 'BUSANZA', '14', '18-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('201', 'BUSARU', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('202', 'BUSEDDE', '45', '8-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('203', 'BUSEMBATIA TOWN COUNCIL', '19', '7-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('204', 'BUSETA', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('205', 'BUSHIKA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('206', 'BUSHIRIBO', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('207', 'BUSHIYI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('208', 'BUSIMBI', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('209', 'BUSIME', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('210', 'BUSITEMA', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('211', 'BUSIU', '32', '26-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('212', 'BUSOBA', '32', '26-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('213', 'BUSOLWE', '36', '60-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('214', 'BUSOLWE TOWN COUNCIL', '36', '60-02-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('215', 'BUSSI', '41', '52-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('216', 'BUSUKUYA', '9', '67-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('217', 'BUSULANI', '10', '51-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('218', 'BUSWALE', '27', '95-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('219', 'BUTAGAYA', '70', '8-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('220', 'BUTALEJA', '35', '60-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('221', 'BUTALEJA TOWN COUNCIL', '35', '60-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('222', 'BUTANDIGA', '10', '51-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('223', 'BUTANSI', '15', '13-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('224', 'BUTAYUNJA', '42', '68-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('225', 'BUTEBA', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('226', 'BUTEBO', '44', '35-02-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('227', 'BUTEMBA', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('228', 'BUTEMBA TOWN COUNCIL', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('229', 'BUTENGA', '24', '98-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('230', 'BUTEZA', '10', '51-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('231', 'BUTIABA', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('232', 'BUTIITI', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('233', 'BUTIRU', '9', '67-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('234', 'BUTOLOOGO', '47', '31-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('235', 'BUTTA', '9', '67-01-15', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('236', 'BUTUNDUZI', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('237', 'BUTUNDUZI TOWN COUNCIL', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('238', 'BUTUNGAMA', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('239', 'BUTUNTUMULA', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('240', 'BUVUMA TOWN COUNCIL', '46', '90-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('241', 'BUWAAYA', '33', '49-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('242', 'BUWABWALA', '9', '67-01-16', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('243', 'BUWAGOGO', '9', '67-01-17', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('244', 'BUWALASI', '10', '51-01-15', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('245', 'BUWALI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('246', 'BUWAMA', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('247', 'BUWASA', '10', '51-01-16', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('248', 'BUWENGE', '70', '8-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('249', 'BUWENGE TOWN COUNCIL', '70', '8-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('250', 'BUWUNGA', '28', '24-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('251', 'BUYANGA', '19', '7-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('252', 'BUYANJA', '136', '37-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('253', 'BUYENDE', '12', '83-02-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('254', 'BUYENDE TOWN COUNCIL', '12', '83-02-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('255', 'BUYENGO', '70', '8-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('256', 'BUYINJA', '27', '95-02-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('257', 'BUYOBO', '10', '51-01-17', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('258', 'BWAMBARA', '139', '37-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('259', 'BWAMIRAMIRA', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('260', 'BWANSWA', '17', '16-02-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('261', 'BWEEMA', '46', '90-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('262', 'BWERAMULE', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('263', 'BWEYALE TOWN COUNCIL', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('264', 'BWIJANGA', '22', '25-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('265', 'BWIKARA', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('266', 'BWIKHONGE', '29', '89-01-15', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('267', 'BWONGERA', '71', '34-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('268', 'BYAKABANDA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('269', 'CAWENTE', '93', '1-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('270', 'CENTRAL DIVISION', '38', '4-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('271', 'CHAHI', '14', '18-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('272', 'CHEGERE', '105', '1-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('273', 'CHELEKURA', '1', '35-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('274', 'CHEMA', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('275', 'CHEPKWASTA', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('276', 'CHESOWER', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('277', 'DABANI', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('278', 'DDWANIRO', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('279', 'DIVISION A', '58', '52-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('280', 'DIVISION B', '58', '52-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('281', 'DOKOLO', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('282', 'DOKOLO TOWN COUNCIL', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('283', 'DRAJINI/ARAJIM', '3', '53-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('284', 'DRANYA', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('285', 'DUFILE', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('286', 'EASTERN', '147', '38-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('287', 'ENDINZI', '23', '62-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('288', 'ENGAJU', '20', '109-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('289', 'ENGARI', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('290', 'ERUSSI', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('291', 'GADUMIRE', '30', '64-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('292', 'GALIRAYA', '7', '47-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('293', 'GAMOGO', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('294', 'GAYAZA', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('295', 'GIMARA', '128', '29-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('296', 'GOGONYO', '1', '35-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('297', 'GOMA DIVISION', '116', '32-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('298', 'GOMBE', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('299', 'GOMBE TOWN COUNCIL', '43', '99-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('300', 'GREEK RIVER', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('301', 'GWERI', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('302', 'HAPUUYO', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('303', 'HARUGALI', '18', '3-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('304', 'HIMUTU', '35', '60-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('305', 'IBAARE', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('306', 'IBANDA TOWN COUNCIL', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('307', 'IBUJE', '105', '1-02-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('308', 'IBULANKU', '19', '7-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('309', 'ICHEME', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('310', 'IGOMBE', '19', '7-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('311', 'IHUNGA', '71', '34-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('312', 'IKUMBYA', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('313', 'IMANYIRO', '33', '49-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('314', 'INDUSTRIAL BOROUGH', '111', '26-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('315', 'INOMO', '93', '1-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('316', 'IRIIRI', '8', '107-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('317', 'IRONGO', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('318', 'ISHAKA DIVISION', '38', '4-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('319', 'ISHONGORORO', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('320', 'ISHONGORORO TOWN COUNCIL', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('321', 'ISINGIRO TOWN COUNCIL', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('322', 'ITOJO', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('323', 'ITULA', '128', '29-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('324', 'IVUKULA', '40', '75-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('325', 'IWEMBA', '25', '41-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('326', 'IYOLWA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('327', 'JAGUZI', '33', '49-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('328', 'JANGOKORO', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('329', 'JINJA CENTRAL', '67', '8-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('330', 'KAABONG EAST', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('331', 'KAABONG TOWN COUNCIL', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('332', 'KAABONG WEST', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('333', 'KAASANGOMBE', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('334', 'KAATO', '9', '67-01-18', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('335', 'KABAMBA', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('336', 'KABEI', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('337', 'KABEREBERE TOWN COUNCIL', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('338', 'KABEYWA', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('339', 'KABINGO', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('340', 'KABIRA', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('341', 'KABONERA', '28', '24-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('342', 'KABULASOKE', '60', '91-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('343', 'KABUYANDA', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('344', 'KABUYANDA TOWN COUNCIL', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('345', 'KABWANGASI', '44', '35-02-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('346', 'KABWERI', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('347', 'KABWOHE/ITENDERO TOWN COUNCIL', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('348', 'KACHEERA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('349', 'KACHERI', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('350', 'KACHONGA', '35', '60-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('351', 'KADAMA', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('352', 'KADUNGULU', '77', '97-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('353', 'KAGADI', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('354', 'KAGADI TOWN COUNCIL', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('355', 'KAGAMBA (BUYAMBA)', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('356', 'KAGANGO', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('357', 'KAGONGI', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('358', 'KAGULU', '11', '83-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('359', 'KAGUMU', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('360', 'KAKABARA', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('361', 'KAKANJU', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('362', 'KAKIIKA', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('363', 'KAKINDO', '17', '16-02-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('364', 'KAKINDU', '42', '68-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('365', 'KAKIRA TOWN COUNCIL', '45', '8-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('366', 'KAKIRI', '41', '52-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('367', 'KAKIRI TOWN COUNCIL', '41', '52-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('368', 'KAKOBA', '112', '27-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('369', 'KAKOMONGOLE', '53', '56-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('370', 'KAKOOGE', '120', '44-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('371', 'KAKORO', '44', '35-02-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('372', 'KAKUUTO', '72', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('373', 'KALAGALA', '6', '23-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('374', 'KALAMBA', '43', '99-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('375', 'KALANGAALO', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('376', 'KALAPATA', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('377', 'KALIIRO', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('378', 'KALIRO TOWN COUNCIL', '30', '64-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('379', 'KALISIZO', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('380', 'KALISIZO TOWN COUNCIL', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('381', 'KALONGO', '120', '44-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('382', 'KALUNGI', '120', '44-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('383', 'KALUNGU', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('384', 'KALUNGU TOWN COUNCIL', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('385', 'KALWANA', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('386', 'KAMDINI', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('387', 'KAMEKE', '1', '35-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('388', 'KAMET', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('389', 'KAMION', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('390', 'KAMIRA', '6', '23-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('391', 'KAMMENGO', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('392', 'KAMUDA', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('393', 'KAMUGE', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('394', 'KAMUKUZI', '112', '27-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('395', 'KAMULI TOWN COUNCIL', '15', '13-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('396', 'KANABA', '14', '18-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('397', 'KANGAI', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('398', 'KANGINIMA', '44', '35-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('399', 'KANGO', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('400', 'KANGULUMIRA', '123', '47-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('401', 'KANONI', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('402', 'KANONI TOWN COUNCIL', '60', '91-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('403', 'KANYABWANGA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('404', 'KANYARYERU', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('405', 'KANYUM', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('406', 'KAPCHESOMBE', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('407', 'KAPCHORWA TOWN COUNCIL', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('408', 'KAPEDO', '56', '63-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('409', 'KAPEEKA', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('410', 'KAPEKE', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('411', 'KAPELEBYONG', '74', '58-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('412', 'KAPIR', '122', '108-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('413', 'KAPRORON', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('414', 'KAPSINDA', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('415', 'KAPTANYA', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('416', 'KAPTERERWO', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('417', 'KAPTERET', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('418', 'KAPTUM', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('419', 'KAPUJAN', '149', '43-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('420', 'KAPYANGA', '25', '41-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('421', 'KARENGA(NAPORE)', '56', '63-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('422', 'KARUGUTU', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('423', 'KARUJUBU DIVISION', '107', '25-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('424', 'KARUNGU', '20', '109-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('425', 'KASAALI', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('426', 'KASAANA', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('427', 'KASAGAMA', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('428', 'KASAMBYA', '17', '16-02-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('429', 'KASANJE', '41', '52-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('430', 'KASASA', '72', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('431', 'KASASIRA', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('432', 'KASAWO', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('433', 'KASEREM', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('434', 'KASHANGURA', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('435', 'KASHARE', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('436', 'KASHENSHERO', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('437', 'KASHENSHERO TOWN COUNCIL', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('438', 'KASHONGI', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('439', 'KASHUMBA', '23', '62-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('440', 'KASITU', '18', '3-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('441', 'KASODO', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('442', 'KASSANDA', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('443', 'KASULE', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('444', 'KATABI', '41', '52-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('445', 'KATAKWI', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('446', 'KATAKWI TOWN COUNCIL', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('447', 'KATANDA', '79', '112-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('448', 'KATENGA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('449', 'KATERERA', '79', '112-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('450', 'KATERERA TOWN COUNCIL', '79', '112-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('451', 'KATETA', '144', '97-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('452', 'KATHILE', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('453', 'KATIKAMU', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('454', 'KATIKEKILE', '108', '28-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('455', 'KATINE', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('456', 'KATOOKE', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('457', 'KATOOKE TOWN COUNCIL', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('458', 'KATUNGURU', '34', '112-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('459', 'KATWE/BUTEGO', '106', '24-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('460', 'KAWALAKOL', '56', '63-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('461', 'KAWOLO', '21', '82-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('462', 'KAWOWO', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('463', 'KAYONZA', '141', '34-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('464', 'KAYUNGA', '123', '47-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('465', 'KAYUNGA TOWN COUNCIL', '123', '47-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('466', 'KAZO', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('467', 'KAZO TOWN COUNCIL', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('468', 'KEBISONI', '136', '37-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('469', 'KEI', '3', '53-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('470', 'KEIHANGARA', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('471', 'KENSHUNGA', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('472', 'KERWA', '3', '53-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('473', 'KHABUTOOLA', '9', '67-01-19', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('474', 'KIBAALE', '40', '75-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('475', 'KIBAALE TOWN COUNCIL', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('476', 'KIBALE', '44', '35-02-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('477', 'KIBALINGA', '75', '31-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('478', 'KIBANDA', '72', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('479', 'KIBATSI', '71', '34-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('480', 'KIBIBI', '43', '99-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('481', 'KIBIGA', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('482', 'KIBINGE', '24', '98-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('483', 'KIBINGO TOWN COUNCIL', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('484', 'KIBOGA TOWN COUNCIL', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('485', 'KIBUKU', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('486', 'KIBUKU TOWN COUNCIL', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('487', 'KIBUUKU TOWN COUNCIL', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('488', 'KICHWAMBA', '34', '112-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('489', 'KICUZI', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('490', 'KIDERA', '12', '83-02-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('491', 'KIFAMBA', '72', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('492', 'KIGANDA', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('493', 'KIGANDALO', '33', '49-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('494', 'KIGANDO', '75', '31-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('495', 'KIGARAALE', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('496', 'KIGARAMA', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('497', 'KIGULYA DIVISION', '107', '25-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('498', 'KIGUMBA', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('499', 'KIGUMBA TOWN COUNCIL', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('500', 'KIGWERA', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('501', 'KIHUNGYA', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('502', 'KIHUURA', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('503', 'KIJOMORO', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('504', 'KIJONGO', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('505', 'KIKAGATE', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('506', 'KIKAMULO', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('507', 'KIKANDWA', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('508', 'KIKATSI', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('509', 'KIKYENKYE', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('510', 'KIKYUSA', '6', '23-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('511', 'KIMAANYA/KYABAKUZA', '106', '24-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('512', 'KIMAKA /MPUMUDDE', '67', '8-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('513', 'KIMENGO', '37', '25-02-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('514', 'KIMENYEDDE', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('515', 'KINGO', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('516', 'KINONI', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('517', 'KINUUKA', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('518', 'KINYOGOGA', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('519', 'KIRA TOWN COUNCIL', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('520', 'KIREWA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('521', 'KIRIKA', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('522', 'KIRINGENTE', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('523', 'KIRUGU', '79', '112-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('524', 'KIRUHURA TOWN COUNCIL', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('525', 'KIRUMBA', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('526', 'KIRUMYA', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('527', 'KIRUNDO', '14', '18-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('528', 'KIRYANDONGO', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('529', 'KIRYANDONGO TOWN COUNCIL', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('530', 'KIRYANGA', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('531', 'KISEKKA', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('532', 'KISIITA', '16', '16-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('533', 'KISOJO', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('534', 'KISOKO', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('535', 'KISORO TOWN COUNCIL', '14', '18-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('536', 'KISOZI', '51', '13-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('537', 'KISUBBA', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('538', 'KITABONA', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('539', 'KITAGATA', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('540', 'KITANDA', '24', '98-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('541', 'KITAWOI', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('542', 'KITAYUNJWA', '15', '13-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('543', 'KITENGA', '47', '31-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('544', 'KITGUM MATIDI', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('545', 'KITGUM TOWN COUNCIL', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('546', 'KITIMBWA', '7', '47-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('547', 'KITTO', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('548', 'KITUMBI', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('549', 'KITUNTU', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('550', 'KITURA', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('551', 'KITYERERA', '33', '49-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('552', 'KIWOKO TOWN COUNCIL', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('553', 'KIYANGA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('554', 'KIYUNI', '47', '31-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('555', 'KIZIBA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('556', 'KIZIBA(MASULIITA)', '41', '52-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('557', 'KOBOKO TOWN COUNCIL', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('558', 'KOBWIN', '122', '108-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('559', 'KOCH-GOMA', '126', '110-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('560', 'KOCHI', '3', '53-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('561', 'KOLE TOWN COUNCIL', '89', '103-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('562', 'KOOME ISLANDS', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('563', 'KORO', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('564', 'KORTEK', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('565', 'KOTIDO', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('566', 'KOTIDO TOWN COUNCIL', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('567', 'KUCWINY', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('568', 'KUJU', '2', '58-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('569', 'KULUBA', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('570', 'KULULU', '3', '53-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('571', 'KUMI', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('572', 'KUMI TOWN COUNCIL', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('573', 'KURU', '3', '53-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('574', 'KWANYIY', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('575', 'KWAPA', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('576', 'KWERA', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('577', 'KWOSIR', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('578', 'KYABAKARA', '79', '112-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('579', 'KYABUGIMBI', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('580', 'KYAKABADIIMA', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('581', 'KYALULANGIRA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('582', 'KYAMBOGO/BUSUKUMA', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('583', 'KYAMPISI', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('584', 'KYAMUHUNGA', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('585', 'KYAMULIBWA', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('586', 'KYANAISOKE', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('587', 'KYANAMUKAKA', '28', '24-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('588', 'KYANGYENYI', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('589', 'KYANKWANZI', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('590', 'KYARUSOZI', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('591', 'KYARUSOZI TOWN COUNCIL', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('592', 'KYATEREKERA', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('593', 'KYAZANGA', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('594', 'KYAZANGA TOWN COUNCIL', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('595', 'KYEBANDO', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('596', 'KYEBE', '72', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('597', 'KYEGEGWA', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('598', 'KYEGEGWA TOWN COUNCIL', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('599', 'KYEGONZA', '60', '91-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('600', 'KYEIZOBA', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('601', 'KYENJOJO TOWN COUNCIL', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('602', 'KYENZIGE', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('603', 'KYERE', '144', '97-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('604', 'KYESIIGA', '28', '24-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('605', 'KYOTERA TOWN COUNCIL', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('606', 'LABONGO-AMIDA', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('607', 'LABONGO-LAYAMO', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('608', 'LABOR', '77', '97-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('609', 'LAGORO', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('610', 'LAGUTI', '4', '50-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('611', 'LAKWANA', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('612', 'LALOGI', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('613', 'LAMOGI', '87', '71-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('614', 'LAPUL', '4', '50-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('615', 'LAROO', '61', '5-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('616', 'LAROPI', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('617', 'LATANYA', '4', '50-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('618', 'LAYIBI', '61', '5-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('619', 'LEFORI', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('620', 'LIRA', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('621', 'LIRA CENTRAL', '99', '22-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('622', 'LOBALANGIT', '56', '63-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('623', 'LOBULE', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('624', 'LODIKO', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('625', 'LODONGA', '3', '53-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('626', 'LOKOPO', '8', '107-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('627', 'LOKUNG', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('628', 'LOLACHAT', '135', '56-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('629', 'LOLELIA', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('630', 'LOPEI', '8', '107-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('631', 'LOREGAE', '53', '56-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('632', 'LORENGECORA', '8', '107-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('633', 'LORENGEDWAT', '135', '56-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('634', 'LORO', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('635', 'LOTOME', '8', '107-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('636', 'LOYORO', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('637', 'LUDARA', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('638', 'LUGAZI TOWN COUNCIL', '21', '82-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('639', 'LUGUSULU', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('640', 'LUKAYA TOWN COUNCIL', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('641', 'LUKHONGE', '32', '26-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('642', 'LUMINO', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('643', 'LUNYO', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('644', 'LUSHA', '29', '89-01-16', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('645', 'LUWEERO  TOWN COUNCIL', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('646', 'LUWERO', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('647', 'LWABENGE', '73', '100-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('648', 'LWAKHAKHA TOWN COUNCIL', '9', '67-01-20', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('649', 'LWAMAGGWA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('650', 'LWAMATA', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('651', 'LWAMPANGA', '13', '44-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('652', 'LWANDA', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('653', 'LWANKONI', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('654', 'LWASSO', '32', '26-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('655', 'LWEBITAKULI', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('656', 'LWEMIYAGA', '101', '45-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('657', 'LWENGO', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('658', 'LWENGO TOWN COUNCIL', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('659', 'LYANTONDE', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('660', 'LYANTONDE TOWN COUNCIL', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('661', 'MAANYI', '42', '68-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('662', 'MABAALE', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('663', 'MADDU', '60', '91-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('664', 'MADI-OPEI', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('665', 'MADUDU', '47', '31-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('666', 'MAFUBIRA', '45', '8-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('667', 'MAGADA', '40', '75-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('668', 'MAGALE', '9', '67-01-21', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('669', 'MAGAMBO', '34', '112-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('670', 'MAGOLA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('671', 'MAGORO', '149', '43-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('672', 'MAJANJI', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('673', 'MAKINDYE', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('674', 'MAKOKOTO', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('675', 'MAKULUBITA', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('676', 'MAKUUTU', '19', '7-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('677', 'MALABA TOWN COUNCIL', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('678', 'MALANGALA', '42', '68-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('679', 'MALONGO', '33', '49-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('680', 'MANAFWA TOWN COUNCIL', '9', '67-01-22', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('681', 'MANYOGASEKA', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('682', 'MARACHA TOWN COUNCIL', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('683', 'MASABA', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('684', 'MASAFU', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('685', 'MASHA', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('686', 'MASHERUKA', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('687', 'MASINDI PORT', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('688', 'MASINYA', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('689', 'MASIRA', '29', '89-01-17', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('690', 'MATALE', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('691', 'MATANY', '8', '107-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('692', 'MATEETE', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('693', 'MATEETE TOWN COUNCIL', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('694', 'MAYANGA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('695', 'MAYUGE TOWN COUNCIL', '33', '49-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('696', 'MAZIMASA', '35', '60-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('697', 'MBAARE', '23', '62-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('698', 'MBULAMUTI', '51', '13-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('699', 'MELLA', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('700', 'MENDE', '41', '52-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('701', 'MERIKIT', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('702', 'METU', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('703', 'MIDIA', '88', '66-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('704', 'MIDIGO', '3', '53-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('705', 'MIIRYA', '37', '25-02-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('706', 'MIJWALA', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('707', 'MINAKULU', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('708', 'MIRAMBI', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('709', 'MITOOMA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('710', 'MITOOMA TOWN COUNCIL', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('711', 'MITYANA TOWN COUNCIL', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('712', 'MOLO', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('713', 'MORUITA', '53', '56-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('714', 'MORUNGATUNY', '2', '58-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('715', 'MOYO', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('716', 'MOYO TOWN COUNCIL', '154', '29-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('717', 'MOYOK', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('718', 'MPARA', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('719', 'MPASAANA', '16', '16-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('720', 'MPATTA', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('721', 'MPEEFU', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('722', 'MPENJA', '60', '91-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('723', 'MPIGI TOWN COUNCIL', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('724', 'MPUMUDDE', '69', '80-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('725', 'MPUNGE', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('726', 'MPUNGWE', '33', '49-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('727', 'MUBENDE TOWN COUNCIL', '47', '31-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('728', 'MUCHWINI', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('729', 'MUDUMA', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('730', 'MUGARAMA', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('731', 'MUHORRO', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('732', 'MUKONGORO', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('733', 'MUKONO DIVISION', '116', '32-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('734', 'MUKOTO', '9', '67-01-23', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('735', 'MUKUJU', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('736', 'MUKUNGWE', '28', '24-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('737', 'MUKURA', '122', '108-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('738', 'MULAGI', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('739', 'MULANDA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('740', 'MUNARYA', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('741', 'MURAMBA', '14', '18-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('742', 'MURORA', '14', '18-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('743', 'MUTARA', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('744', 'MUTERERE', '25', '41-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('745', 'MUTUMBA', '27', '95-02-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('746', 'MUTUNDA', '82', '92-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('747', 'MUWANGA', '83', '17-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('748', 'MUYEMBE', '29', '89-01-18', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('749', 'MUYEMBE TOWN COUNCIL', '29', '89-01-19', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('750', 'MWIZI', '142', '27-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('751', 'MYANZI', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('752', 'MYENE', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('753', 'NABBALE', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('754', 'NABBONGO', '29', '89-01-20', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('755', 'NABIGASA', '97', '36-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('756', 'NABILATUK', '135', '56-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('757', 'NABINGOOLA', '75', '31-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('758', 'NABISWERA', '13', '44-01-02', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('759', 'NABITENDE', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('760', 'NABUKALU', '25', '41-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('761', 'NABUYOGA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('762', 'NABWERU', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('763', 'NABWEYA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('764', 'NABWIGULU', '15', '13-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('765', 'NADUNGET', '108', '28-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('766', 'NAGOJJE', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('767', 'NAGONGERA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('768', 'NAGONGERA TOWN COUNCIL', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('769', 'NAIRAMBI', '46', '90-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('770', 'NAJJA', '21', '82-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('771', 'NAJJEMBE', '21', '82-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('772', 'NAKALAMA', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('773', 'NAKALOKE', '32', '26-01-15', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('774', 'NAKAPELIMORU', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('775', 'NAKAPIRIPIRIT TOWN COUNCIL', '53', '56-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('776', 'NAKASEKE', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('777', 'NAKASEKE BUTALANGU TOWN COUNCIL', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('778', 'NAKASEKE TOWN COUNCIL', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('779', 'NAKASONGOLA TOWN COUNCIL', '120', '44-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('780', 'NAKATSI', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('781', 'NAKIGO', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('782', 'NAKISUNGA', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('783', 'NAKITOMA', '13', '44-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('784', 'NALONDO', '9', '67-01-24', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('785', 'NALUSALA', '10', '51-01-18', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('786', 'NALUTUNTU', '78', '31-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('787', 'NALWANZA', '103', '78-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('788', 'NAM-OKORA', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('789', 'NAMA', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('790', 'NAMABYA', '9', '67-01-25', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('791', 'NAMALEMBA', '19', '7-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('792', 'NAMALU', '53', '56-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('793', 'NAMANYONYI', '32', '26-01-16', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('794', 'NAMASAGALI', '15', '13-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('795', 'NAMAYINGO TOWN COUNCIL', '27', '95-02-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('796', 'NAMAYUMBA', '41', '52-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('797', 'NAMBALE', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('798', 'NAMBIESO', '93', '1-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('799', 'NAMBOKO', '9', '67-01-26', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('800', 'NAMISUNI', '29', '89-01-21', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('801', 'NAMUGANGA', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('802', 'NAMUGONGO', '30', '64-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('803', 'NAMUNGALWE', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('804', 'NAMUNGO', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('805', 'NAMUTUMBA', '40', '75-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('806', 'NAMUTUMBA TOWN COUNCIL', '40', '75-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('807', 'NAMWENDWA', '15', '13-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('808', 'NAMWIWA', '30', '64-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('809', 'NANGABO', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('810', 'NANKOMA', '25', '41-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('811', 'NANSANA TOWN COUNCIL', '95', '52-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('812', 'NAPAK TOWN COUNCIL', '8', '107-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('813', 'NARWEYO', '17', '16-02-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('814', 'NAWAIKOKE', '30', '64-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('815', 'NAWAMPITI', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('816', 'NAWANDALA', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('817', 'NAWANJOFU', '36', '60-02-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('818', 'NAWANYAGO', '51', '13-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('819', 'NAWANYINGI', '86', '7-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('820', 'NAWEYO', '35', '60-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('821', 'NAZIGO', '123', '47-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('822', 'NDAGWE', '102', '105-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('823', 'NDAIGA', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('824', 'NDEIJA', '142', '27-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('825', 'NDHEW', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('826', 'NDUGUTU', '18', '3-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('827', 'NEBBI', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('828', 'NEBBI TOWN COUNCIL', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('829', 'NGAI', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('830', 'NGAMBA', '18', '3-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('831', 'NGANDO', '43', '99-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('832', 'NGARAMA', '23', '62-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('833', 'NGARIAM', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('834', 'NGENGE', '94', '104-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('835', 'NGETTA', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('836', 'NGOGWE', '21', '82-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('837', 'NGOLERIET', '8', '107-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('838', 'NGOMA', '141', '34-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('839', 'NGOMA TOWN COUNCIL', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('840', 'NGORA', '122', '108-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('841', 'NGORA TOWN COUNCIL', '122', '108-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('842', 'NGWEDO', '31', '73-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('843', 'NJERU TOWN COUNCIL', '21', '82-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('844', 'NKOKONJERU TOWN COUNCIL', '21', '82-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('845', 'NKONDO', '12', '83-02-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('846', 'NKOOKO', '16', '16-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('847', 'NKOZI', '110', '30-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('848', 'NKUNGU', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('849', 'NOMBE', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('850', 'NORTH DIVISION', '114', '28-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('851', 'NORTHERN', '147', '38-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('852', 'NORTHERN BOROUGH', '111', '26-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('853', 'NORTHERN DIVISION', '63', '7-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('854', 'NSAMBYA', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('855', 'NSANGI(MUKONO)', '41', '52-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('856', 'NSASI', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('857', 'NSIIKA TOWN COUNCIL', '20', '109-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('858', 'NSINZE', '40', '75-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('859', 'NTENJERU', '115', '32-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('860', 'NTOTORO', '18', '3-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('861', 'NTUNDA', '121', '32-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('862', 'NTUNGAMO', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('863', 'NTUUSI', '101', '45-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('864', 'NTWETWE TOWN COUNCIL', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('865', 'NYABIHOKO', '71', '34-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('866', 'NYABUBARE', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('867', 'NYABUHARWA', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('868', 'NYABUHIKYE', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('869', 'NYABWISHENYA', '14', '18-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('870', 'NYADRI', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('871', 'NYAHUKA  TOWN COUNCIL', '52', '3-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('872', 'NYAKABANDE', '14', '18-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('873', 'NYAKABIRIZI DIVISION', '38', '4-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('874', 'NYAKAGYEME', '139', '37-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('875', 'NYAKASHASHARA', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('876', 'NYAKAYOJO', '142', '27-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('877', 'NYAKINAMA', '14', '18-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('878', 'NYAKISHANA', '20', '109-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('879', 'NYAKISHENYI', '136', '37-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('880', 'NYAKITUNDA', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('881', 'NYAKYERA', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('882', 'NYAMAREBE', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('883', 'NYAMARUNDA', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('884', 'NYAMARWA', '50', '16-05-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('885', 'NYAMITANGA', '112', '27-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('886', 'NYAMUYANJA', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('887', 'NYANGAHYA DIVISION', '107', '25-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('888', 'NYANKWANZI', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('889', 'NYANTUNGO', '117', '48-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('890', 'NYAPEA', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('891', 'NYARAVUR', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('892', 'NYARUBUYE', '14', '18-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('893', 'NYARUSHANJE', '136', '37-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('894', 'NYARUSIZA', '14', '18-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('895', 'NYENDO/SENYANGE', '106', '24-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('896', 'NYENGA', '21', '82-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('897', 'NYERO', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('898', 'NYIMBWA', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('899', 'NYONDO', '32', '26-01-17', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('900', 'NYUNDO', '14', '18-01-14', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('901', 'OBALANGA', '74', '58-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('902', 'ODEK', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('903', 'ODRAVU', '3', '53-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('904', 'OGILI', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('905', 'OGOM', '4', '50-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('906', 'OGOR', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('907', 'OGUR', '59', '22-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('908', 'OJWINA', '99', '22-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('909', 'OKWALONGWEN', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('910', 'OKWANG', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('911', 'OKWONGODUL', '57', '74-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('912', 'OLEBA', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('913', 'OLILIM', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('914', 'OLOK', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('915', 'OLUFE', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('916', 'OLUVU', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('917', 'OMIYA-ANYIMA', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('918', 'OMODOI', '149', '43-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('919', 'ONGAKO', '130', '5-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('920', 'ONGINO', '92', '21-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('921', 'ONGONGOJA', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('922', 'OPWATETA', '44', '35-02-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('923', 'OROM', '54', '19-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('924', 'ORUM', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('925', 'ORUNGO', '2', '58-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('926', 'OSUKURU', '150', '39-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('927', 'OTUKE TOWN COUNCIL', '131', '86-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('928', 'OTWAL', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('929', 'OYAM TOWN COUNCIL', '132', '76-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('930', 'PABBO', '87', '71-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('931', 'PACHWA', '48', '16-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('932', 'PADER', '4', '50-01-09', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('933', 'PADER TOWN COUNCIL', '4', '50-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('934', 'PADIBE EAST', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('935', 'PADIBE WEST', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('936', 'PAICHO', '5', '5-01-03', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('937', 'PAIDHA', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('938', 'PAIDHA TOWN COUNCIL', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('939', 'PAJULE', '4', '50-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('940', 'PAKANYI', '37', '25-02-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('941', 'PAKWACH', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('942', 'PAKWACH TOWN COUNCIL', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('943', 'PALABEK-GEM', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('944', 'PALABEK-KAL', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('945', 'PALAM', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('946', 'PALARO', '5', '5-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('947', 'PALLISA', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('948', 'PALLISA TOWN COUNCIL', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('949', 'PALOGA', '98', '85-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('950', 'PANYANGARA', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('951', 'PANYANGO', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('952', 'PANYIMUR', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('953', 'PAROMBO', '133', '33-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('954', 'PATIKO', '5', '5-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('955', 'PAYA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('956', 'PECE', '61', '5-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('957', 'PETETE', '44', '35-02-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('958', 'PETTA', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('959', 'PINGIRE', '77', '97-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('960', 'PURANGA', '4', '50-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('961', 'PURONGO', '126', '110-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('962', 'PUTI-PUTI', '134', '35-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('963', 'RAILWAYS', '99', '22-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('964', 'RAKAI TOWN COUNCIL', '91', '36-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('965', 'RENGEN', '66', '20-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('966', 'RIWO', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('967', 'ROMOGI', '3', '53-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('968', 'RUBAARE', '141', '34-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('969', 'RUBINDI', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('970', 'RUBIRIZI TOWN COUNCIL', '34', '112-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('971', 'RUBONGI', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('972', 'RUBOROGOTA', '65', '62-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('973', 'RUGAAGA', '23', '62-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('974', 'RUGANDO', '142', '27-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('975', 'RUGARAMA', '141', '34-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('976', 'RUGASHARI', '49', '16-04-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('977', 'RUHAAMA', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('978', 'RUHINDA', '139', '37-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('979', 'RUHUMURO', '64', '4-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('980', 'RUKIRI', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('981', 'RUKONI EAST', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('982', 'RUKONI WEST', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('983', 'RUPA', '108', '28-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('984', 'RUREHE', '138', '106-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('985', 'RUSHANGO TOWN COUNCIL', '62', '61-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('986', 'RUSHASHA', '23', '62-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('987', 'RUTOTO', '34', '112-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('988', 'RUYONZA', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('989', 'RWABYATA', '13', '44-01-04', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('990', 'RWANYAMAHEMBE', '76', '27-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('991', 'RWEBISENGO', '124', '96-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('992', 'RWEIKINIRO', '137', '34-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('993', 'RWEMIKOMA', '81', '65-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('994', 'RWENGWE', '20', '109-01-08', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('995', 'RWENTUHA', '96', '84-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('996', 'RYERU', '34', '112-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('997', 'SANGA', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('998', 'SANGA TOWN COUNCIL', '127', '65-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('999', 'SEMUTO', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1000', 'SEMUTO TOWN COUNCIL', '119', '69-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1001', 'SENENDET', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1002', 'SERERE TOWN COUNCIL', '144', '97-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1003', 'SERERE/OLIO', '144', '97-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1004', 'SHUUKU', '145', '101-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1005', 'SIBANGA', '9', '67-01-27', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1006', 'SIDOK (KAPOTH)', '55', '63-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1007', 'SIGULU ISLANDS', '26', '95-01-01', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1008', 'SIKUDA', '143', '42-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1009', 'SIMU', '29', '89-01-22', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1010', 'SINDILA', '18', '3-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1011', 'SIPI', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1012', 'SIRONKO TOWN COUNCIL', '10', '51-01-19', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1013', 'SISIYI', '29', '89-01-23', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1014', 'SISUNI', '9', '67-01-28', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1015', 'SOPSOP', '153', '39-03-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1016', 'SOROTI', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1017', 'SOUTH DIVISION', '114', '28-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1018', 'SSEKANYONYI', '113', '68-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1019', 'SSEMBABULE TOWN COUNCIL', '109', '45-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1020', 'SSI', '21', '82-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1021', 'SSISA', '41', '52-01-10', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1022', 'SUAM', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1023', 'TAPAC', '108', '28-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1024', 'TARA', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1025', 'TEGERES', '148', '14-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1026', 'TIRINYI', '85', '102-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1027', 'TOROMA', '149', '43-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1028', 'TORORO EASTERN', '151', '39-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1029', 'TORORO WESTERN', '151', '39-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1030', 'TSEKULULU', '9', '67-01-29', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1031', 'TUBUR', '146', '38-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1032', 'TULEL', '90', '59-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1033', 'UNYAMA', '5', '5-01-06', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1034', 'USUK', '152', '43-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1035', 'WABINYONYI', '120', '44-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1036', 'WADELAI', '68', '33-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1037', 'WAIBUGA', '100', '94-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1038', 'WAIRASA', '33', '49-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1039', 'WAKISI', '21', '82-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1040', 'WAKISO', '41', '52-01-11', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1041', 'WAKISO TOWN COUNCIL', '41', '52-01-12', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1042', 'WAKYATO', '118', '69-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1043', 'WALUKUBA/MASESE', '67', '8-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1044', 'WANALE', '32', '26-01-18', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1045', 'WANALE BOROUGH', '111', '26-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1046', 'WANKOLE', '51', '13-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1047', 'WARR', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1048', 'WATTUBA', '84', '93-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1049', 'WERA', '2', '58-01-07', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1050', 'WESTERN', '147', '38-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1051', 'WESTERN DIVISION', '125', '34-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1052', 'WESWA', '9', '67-01-30', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1053', 'WOBULENZI TOWN COUNCIL', '80', '23-02-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1054', 'YIVU', '104', '77-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1055', 'YUMBE TOWN COUNCIL', '3', '53-01-13', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1056', 'ZESUI', '10', '51-01-20', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1057', 'ZEU', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1058', 'ZIROBWE', '6', '23-01-05', '2014-10-08 13:09:40', '2014-10-08 13:09:40');
INSERT INTO `sub_county` VALUES ('1059', 'ZOMBO TOWN COUNCIL', '129', '87-01-', '2014-10-08 13:09:40', '2014-10-08 13:09:40');

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(50) DEFAULT NULL,
  `lastActivityDate` datetime DEFAULT NULL,
  `userType` int(11) DEFAULT NULL COMMENT 'Options: 1 - Staff, 2 - Student, 3 - Prospective Student',
  `email` varchar(100) DEFAULT NULL,
  `comment` varchar(250) DEFAULT NULL,
  `isApproved` tinyint(4) NOT NULL,
  `lastLoginDate` datetime DEFAULT NULL,
  `lastPasswordChangeDate` datetime DEFAULT NULL,
  `isLockedOut` tinyint(4) NOT NULL,
  `failedPasswordAttemptsCount` int(11) DEFAULT NULL,
  `failedPasswordAttemptWindowStart` datetime DEFAULT NULL,
  `password` varchar(250) DEFAULT NULL,
  `passwordKey` varchar(250) DEFAULT NULL,
  `isFirstTimeUser` tinyint(4) DEFAULT NULL,
  `profilePhotoName` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3145 DEFAULT CHARSET=latin1 COMMENT='Stores all the users of the application';

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('2', 'admin', '2016-03-23 15:06:12', '1', 'mawebejohnbosco@gmail.com', '', '1', '2016-03-23 15:06:12', '2014-10-21 10:45:58', '0', '1', '2015-08-12 12:35:51', 'AIGRqTTqg2Ra2ke58mXEY2+C0Wi1Spgm45ElCpBkIXKg77r1HHM+r5pCJIc5qItF6g==', 's5k4B8IJ+VSvGRPJAiVo+A==', '1', '');
INSERT INTO `user` VALUES ('3144', '09/327/002/D/1', '2016-03-21 14:25:46', '2', 'abanda67@yahoo.com', '', '1', '2016-03-21 14:25:46', '2015-05-21 20:33:10', '0', '1', '2015-05-21 20:32:27', '0w3c0WhIEjSfpqmwfR05Gg==|2|X3ukB1YY0hGwMVbgwy7KkQ==', 'X3ukB1YY0hGwMVbgwy7KkQ==', '1', '09327002D1.jpg');

-- ----------------------------
-- Table structure for `user_role`
-- ----------------------------
DROP TABLE IF EXISTS `user_role`;
CREATE TABLE `user_role` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `userId` int(10) unsigned NOT NULL,
  `accessRoleId` int(10) unsigned DEFAULT NULL,
  `roleId` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FKUserRoleUser` (`userId`),
  KEY `FKUserRoleAccessRol` (`accessRoleId`),
  KEY `FKUserRoleRole` (`roleId`),
  KEY `userId` (`userId`),
  KEY `roleId` (`roleId`),
  KEY `accessRoleId` (`accessRoleId`),
  CONSTRAINT `FK5E3BB4B322CC2C02` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`),
  CONSTRAINT `FK5E3BB4B34022A331` FOREIGN KEY (`accessRoleId`) REFERENCES `access_role` (`id`),
  CONSTRAINT `FK5E3BB4B365A1D7D8` FOREIGN KEY (`userId`) REFERENCES `user` (`id`),
  CONSTRAINT `FKUserRoleAccessRol` FOREIGN KEY (`accessRoleId`) REFERENCES `access_role` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FKUserRoleRole` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `FKUserRoleUser` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Stores All Roles n the application that the user requires to';

-- ----------------------------
-- Records of user_role
-- ----------------------------
