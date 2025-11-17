SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS observation;
DROP TABLE IF EXISTS action_arrosage;
DROP TABLE IF EXISTS action;
DROP TABLE IF EXISTS mesure;
DROP TABLE IF EXISTS capteur;
DROP TABLE IF EXISTS plantes;
DROP TABLE IF EXISTS specimen;
DROP TABLE IF EXISTS relais;
DROP TABLE IF EXISTS zone;
DROP TABLE IF EXISTS utilisateur;
DROP TABLE IF EXISTS espece;
DROP TABLE IF EXISTS luminosite_categorie;
DROP TABLE IF EXISTS humidite_sol_categorie;

SET FOREIGN_KEY_CHECKS = 1;

CREATE DATABASE IF NOT EXISTS serre;
USE serre;

-- ============================================
-- Soil_Humidity_Category
-- ============================================
CREATE TABLE soil_humidity_categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    min_humidity_pct FLOAT,
    max_humidity_pct FLOAT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- Specimen
-- ============================================
CREATE TABLE specimens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    soil_humidity_cat_id INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

     CONSTRAINT fk_specimen_humidity
        FOREIGN KEY (soil_humidity_cat_id) REFERENCES soil_humidity_categories(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- Zone_Category
-- ============================================

CREATE TABLE zone_categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    humidity_min_pct FLOAT NOT NULL,
    humidity_max_pct FLOAT NOT NULL,
    luminosity_min_lux FLOAT NOT NULL,
    luminosity_max_lux FLOAT NOT NULL,
    temperature_min_c FLOAT NOT NULL,
    temperature_max_c FLOAT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
-- Zone
-- ============================================
CREATE TABLE zones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    description TEXT,
    zone_category_id INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_zone_zone_category
        FOREIGN KEY (zone_category_id) REFERENCES zone_categories(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- Plant
-- ============================================
CREATE TABLE plants (
    id INT AUTO_INCREMENT PRIMARY KEY,
    acquired_date DATE NOT NULL,
    specimen_id INT NOT NUll,
    zone_id INT NOT NULL,
    mom_id INT NULL,
    description TEXT,
    is_active BOOLEAN NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_plant_specimen
        FOREIGN KEY (specimen_id) REFERENCES specimens(id)
        ON DELETE RESTRICT ON UPDATE CASCADE,

    CONSTRAINT fk_plant_zone
        FOREIGN KEY (zone_id) REFERENCES zones(id)
        ON DELETE RESTRICT ON UPDATE CASCADE,

    CONSTRAINT fk_plant_mom
        FOREIGN KEY (mom_id) REFERENCES plants(id)
        ON DELETE SET NULL ON UPDATE CASCADE
);



-- ============================================
-- Sensor
-- ============================================
CREATE TABLE sensors (
    id VARCHAR(50) PRIMARY KEY,
    description text,
    type VARCHAR(50) NOT NULL,
    last_seen BOOLEAN,
    zone_id INT,
    is_active BOOLEAN NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_sensor_zone
        FOREIGN KEY (zone_id) REFERENCES zones(id)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- ============================================
-- Sensor_Alert
-- ============================================
CREATE TABLE sensor_alerts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    reason VARCHAR(50) NOT NULL,
    sensor_id VARCHAR(50) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_sensor_alert_sensor
        FOREIGN KEY (sensor_id) REFERENCES sensors(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ============================================
-- OBSERVATION  
-- ============================================
CREATE TABLE observations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    plant_id INT,
    rating VARCHAR(20),
    comments TEXT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_observation_plant
        FOREIGN KEY (plant_id) REFERENCES plants(id)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- ============================================
-- Fertilizer
-- ============================================
CREATE TABLE fertilizers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    type VARCHAR(50) NOT NULL,
    plant_id INT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_fertilizer_plant
        FOREIGN KEY (plant_id) REFERENCES plants(id)
        ON DELETE SET NULL ON UPDATE CASCADE
);

-- ============================================
-- Watering
-- ============================================
CREATE TABLE waterings (
    id INT AUTO_INCREMENT PRIMARY KEY,
    hum_pct_before FLOAT NOT NULL,
    hum_pct_after FLOAT NOT NULL,
    water_quantity_ml INT NOT NULL,
    plant_id INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_watering_specimen
        FOREIGN KEY (plant_id) REFERENCES plants(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ============================================
-- Zone_Pressure_Record
-- ============================================
CREATE TABLE zone_pressure_records (
    id INT AUTO_INCREMENT PRIMARY KEY,
    recorded_hPa FLOAT NOT NULL,
    zone_id INT NOT NULL,
    sensor_id VARCHAR(50) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT fk_zone_pressure_sensor
        FOREIGN KEY (sensor_id) REFERENCES sensors(id)
        ON DELETE CASCADE ON UPDATE CASCADE,
    
    CONSTRAINT fk_zone_pressure_zone
        FOREIGN KEY (zone_id) REFERENCES zones(id)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- ============================================
-- Zone_Alert
-- ============================================
CREATE TABLE zone_alerts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    zone_id INT NOT NULL,
    reason VARCHAR(50) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

     CONSTRAINT fk_zone_alert_zone
        FOREIGN KEY (zone_id) REFERENCES zones(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- Plant_Alert
-- ============================================
CREATE TABLE plant_alerts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    plant_id INT NOT NULL,
    reason VARCHAR(50) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

     CONSTRAINT fk_alert_plant_plant
        FOREIGN KEY (plant_id) REFERENCES plants(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- Zone_Record
-- ============================================
CREATE TABLE zone_records (
    id INT AUTO_INCREMENT PRIMARY KEY,
    record FLOAT NOT NULL,  
    in_range BOOLEAN NOT NULL,
    zone_id INT NOT NULL,
    sensor_id VARCHAR(50) NOT NULL,
    type VARCHAR(25) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

     CONSTRAINT fk_zone_record_zone
        FOREIGN KEY (zone_id) REFERENCES zones(id)
        ON DELETE RESTRICT ON UPDATE CASCADE,

     CONSTRAINT fk_zone_record_sensor
        FOREIGN KEY (sensor_id) REFERENCES sensors(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
-- Plant_Humidity_Record
-- ============================================
CREATE TABLE plant_humidity_records (
    id INT AUTO_INCREMENT PRIMARY KEY,
    record_pct FLOAT NOT NULL,  
    in_range BOOLEAN NOT NULL,
    plant_id INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

     CONSTRAINT fk_plante_humidite_record_plant
        FOREIGN KEY (plant_id) REFERENCES plants(id)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

