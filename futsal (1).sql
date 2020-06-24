-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 16, 2020 at 12:06 PM
-- Server version: 10.4.6-MariaDB
-- PHP Version: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `futsal`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `id_admin` varchar(255) NOT NULL,
  `nama_admin` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `level` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id_admin`, `nama_admin`, `password`, `level`) VALUES
('admin', 'admin', 'admin', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `detail_reservasi`
--

CREATE TABLE `detail_reservasi` (
  `id_reservasi` varchar(255) NOT NULL,
  `id_lapangan` varchar(255) NOT NULL,
  `waktureservasi` time NOT NULL,
  `nama_pelanggan` varchar(255) NOT NULL,
  `telepon` varchar(255) NOT NULL,
  `tanggal_main` date NOT NULL,
  `lama_main` varchar(255) NOT NULL,
  `status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `lapangan`
--

CREATE TABLE `lapangan` (
  `id_lapangan` varchar(255) NOT NULL,
  `harga_lapangan` varchar(255) NOT NULL,
  `jenis_rumput` varchar(255) NOT NULL,
  `DP` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `lapangan`
--

INSERT INTO `lapangan` (`id_lapangan`, `harga_lapangan`, `jenis_rumput`, `DP`) VALUES
('Lapangan001', '20000', 'Sintetis', '15000'),
('Lapangan002', '50000', 'Asli', '30000');

-- --------------------------------------------------------

--
-- Table structure for table `reservasi`
--

CREATE TABLE `reservasi` (
  `id_reservasi` varchar(255) NOT NULL,
  `tanggalreservasi` date NOT NULL,
  `waktureservasi` time NOT NULL,
  `id_lapangan` varchar(255) NOT NULL,
  `nama_pelanggan` varchar(255) NOT NULL,
  `alamat` varchar(255) NOT NULL,
  `telepon` varchar(255) NOT NULL,
  `tanggal_main` date NOT NULL,
  `lama_main` varchar(255) NOT NULL,
  `status` varchar(255) NOT NULL,
  `dp` int(50) DEFAULT NULL,
  `jam_main` varchar(50) NOT NULL,
  `jadwal` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `reservasi`
--

INSERT INTO `reservasi` (`id_reservasi`, `tanggalreservasi`, `waktureservasi`, `id_lapangan`, `nama_pelanggan`, `alamat`, `telepon`, `tanggal_main`, `lama_main`, `status`, `dp`, `jam_main`, `jadwal`) VALUES
('RE200415001', '2020-04-15', '09:17:54', 'Lapangan002', 'maling', 'qweqweq', '123123213', '2020-04-16', '3 Jam', 'Lunas', 0, '10:00 - 11:00, 12:00 - 13:00, 11:00 - 12:00, ', '01 '),
('RE200415002', '2020-04-15', '09:50:22', 'Lapangan001', 'mang sane', 'ewewe', '221321', '2020-04-16', '2 Jam', 'Lunas', 0, '17:00 - 18:00, 18:00 - 19:00, ', '10 11 '),
('RE200416003', '2020-04-16', '04:27:12', 'Lapangan002', 'sasasa', 'sasasas', '12121212', '2020-04-16', ' Jam', 'Lunas', 0, '09:00 - 10:00, ', '02 '),
('RE200416004', '2020-04-16', '04:59:39', 'Lapangan002', 'bang ke', 'adssd', '23123123', '2020-04-16', '2 Jam', 'DP', 80000, '10:00 - 11:00, 12:00 - 13:00, ', '03  05  '),
('RE200416005', '2020-04-16', '05:00:10', 'Lapangan002', 'bang sat', 'fwsfe', '123123123', '2020-04-16', '2 Jam', 'DP', 60000, '15:00 - 16:00, 13:00 - 14:00, ', '08  06  '),
('RE200416006', '2020-04-16', '05:04:40', 'Lapangan002', 'saba', 'asdasd', '213123', '2020-04-16', '2 Jam', 'Lunas', 0, '23:00 - 00:00, 21:00 - 22:00, ', '16  14  ');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`id_admin`);

--
-- Indexes for table `lapangan`
--
ALTER TABLE `lapangan`
  ADD PRIMARY KEY (`id_lapangan`);

--
-- Indexes for table `reservasi`
--
ALTER TABLE `reservasi`
  ADD PRIMARY KEY (`id_reservasi`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
