-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.4.22-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных airportdb
DROP DATABASE IF EXISTS `airportdb`;
CREATE DATABASE IF NOT EXISTS `airportdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `airportdb`;

-- Дамп структуры для таблица airportdb.airplane
DROP TABLE IF EXISTS `airplane`;
CREATE TABLE IF NOT EXISTS `airplane` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `airplane_number` varchar(255) DEFAULT NULL,
  `airplane_brand_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_airplane_brand` (`airplane_brand_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы airportdb.airplane: ~0 rows (приблизительно)
DELETE FROM `airplane`;
/*!40000 ALTER TABLE `airplane` DISABLE KEYS */;
/*!40000 ALTER TABLE `airplane` ENABLE KEYS */;

-- Дамп структуры для таблица airportdb.airplanebrand
DROP TABLE IF EXISTS `airplanebrand`;
CREATE TABLE IF NOT EXISTS `airplanebrand` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы airportdb.airplanebrand: ~4 rows (приблизительно)
DELETE FROM `airplanebrand`;
/*!40000 ALTER TABLE `airplanebrand` DISABLE KEYS */;
INSERT INTO `airplanebrand` (`id`, `name`) VALUES
	(1, 'Аерофлот'),
	(2, 'ИжАвиа'),
	(3, 'Россия'),
	(4, 'S7');
/*!40000 ALTER TABLE `airplanebrand` ENABLE KEYS */;

-- Дамп структуры для таблица airportdb.airport
DROP TABLE IF EXISTS `airport`;
CREATE TABLE IF NOT EXISTS `airport` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Дамп данных таблицы airportdb.airport: ~0 rows (приблизительно)
DELETE FROM `airport`;
/*!40000 ALTER TABLE `airport` DISABLE KEYS */;
/*!40000 ALTER TABLE `airport` ENABLE KEYS */;

-- Дамп структуры для таблица airportdb.flight
DROP TABLE IF EXISTS `flight`;
CREATE TABLE IF NOT EXISTS `flight` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `airport_id` int(11) DEFAULT NULL,
  `airplane_id` int(11) DEFAULT NULL,
  `departure_date` datetime DEFAULT NULL,
  `arrival_date` datetime DEFAULT NULL,
  `available_seats` int(11) DEFAULT NULL,
  `price` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_airport` (`airport_id`),
  KEY `fk_airplane` (`airplane_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы airportdb.flight: ~0 rows (приблизительно)
DELETE FROM `flight`;
/*!40000 ALTER TABLE `flight` DISABLE KEYS */;
INSERT INTO `flight` (`id`, `airport_id`, `airplane_id`, `departure_date`, `arrival_date`, `available_seats`, `price`) VALUES
	(1, 123, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', 0, 0),
	(2, 0, 0, '2023-06-12 01:01:41', '2000-06-12 01:01:41', 0, 0);
/*!40000 ALTER TABLE `flight` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
