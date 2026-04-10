-- =============================================
-- AutoNext Platform - India Locations Seed Data
-- Using GUID as Primary Key
-- Execute this in pgAdmin Query Tool
-- =============================================

-- Enable UUID extension (if not already enabled)
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- =============================================
-- DROP EXISTING TABLES (if needed - BE CAREFUL!)
-- =============================================
-- DROP TABLE IF EXISTS city_areas CASCADE;
-- DROP TABLE IF EXISTS locations CASCADE;
-- DROP VIEW IF EXISTS vw_location_search CASCADE;
-- DROP VIEW IF EXISTS vw_city_with_areas CASCADE;

-- =============================================
-- CREATE LOCATIONS TABLE WITH GUID
-- =============================================
CREATE TABLE IF NOT EXISTS locations (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    country_name VARCHAR(100) NOT NULL,
    country_code VARCHAR(5) NOT NULL,
    state_name VARCHAR(100) NOT NULL,
    state_code VARCHAR(10) NOT NULL,
    city_name VARCHAR(100) NOT NULL,
    district VARCHAR(100),
    pincode VARCHAR(10),
    latitude DECIMAL(10, 8),
    longitude DECIMAL(11, 8),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- =============================================
-- CREATE CITY_AREAS TABLE WITH GUID
-- =============================================
CREATE TABLE IF NOT EXISTS city_areas (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    location_id UUID NOT NULL REFERENCES locations(id) ON DELETE CASCADE,
    area_name VARCHAR(200) NOT NULL,
    area_code VARCHAR(20),
    pincode VARCHAR(10),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (location_id) REFERENCES locations(id)
);

-- =============================================
-- INSERT LOCATIONS (Top 100+ Cities in India)
-- Using fixed GUIDs for easy reference
-- =============================================

-- MAHARASHTRA (MH)
INSERT INTO locations (id, country_name, country_code, state_name, state_code, city_name, district, pincode, latitude, longitude, is_active, created_at) VALUES
('11111111-1111-1111-1111-111111111101', 'India', 'IN', 'Maharashtra', 'MH', 'Mumbai', 'Mumbai City', '400001', 19.0760, 72.8777, true, NOW()),
('11111111-1111-1111-1111-111111111102', 'India', 'IN', 'Maharashtra', 'MH', 'Pune', 'Pune', '411001', 18.5204, 73.8567, true, NOW()),
('11111111-1111-1111-1111-111111111103', 'India', 'IN', 'Maharashtra', 'MH', 'Nagpur', 'Nagpur', '440001', 21.1458, 79.0882, true, NOW()),
('11111111-1111-1111-1111-111111111104', 'India', 'IN', 'Maharashtra', 'MH', 'Nashik', 'Nashik', '422001', 19.9975, 73.7898, true, NOW()),
('11111111-1111-1111-1111-111111111105', 'India', 'IN', 'Maharashtra', 'MH', 'Aurangabad', 'Aurangabad', '431001', 19.8762, 75.3433, true, NOW()),
('11111111-1111-1111-1111-111111111106', 'India', 'IN', 'Maharashtra', 'MH', 'Thane', 'Thane', '400601', 19.2183, 72.9781, true, NOW()),
('11111111-1111-1111-1111-111111111107', 'India', 'IN', 'Maharashtra', 'MH', 'Navi Mumbai', 'Thane', '400706', 19.0330, 73.0297, true, NOW()),
('11111111-1111-1111-1111-111111111108', 'India', 'IN', 'Maharashtra', 'MH', 'Solapur', 'Solapur', '413001', 17.6599, 75.9064, true, NOW()),
('11111111-1111-1111-1111-111111111109', 'India', 'IN', 'Maharashtra', 'MH', 'Kolhapur', 'Kolhapur', '416001', 16.7050, 74.2433, true, NOW()),
('11111111-1111-1111-1111-111111111110', 'India', 'IN', 'Maharashtra', 'MH', 'Amravati', 'Amravati', '444601', 20.9374, 77.7796, true, NOW()),

-- DELHI (DL)
('22222222-2222-2222-2222-222222222201', 'India', 'IN', 'Delhi', 'DL', 'New Delhi', 'Central Delhi', '110001', 28.6139, 77.2090, true, NOW()),
('22222222-2222-2222-2222-222222222202', 'India', 'IN', 'Delhi', 'DL', 'South Delhi', 'South Delhi', '110017', 28.5245, 77.1855, true, NOW()),
('22222222-2222-2222-2222-222222222203', 'India', 'IN', 'Delhi', 'DL', 'North Delhi', 'North Delhi', '110006', 28.6660, 77.2168, true, NOW()),
('22222222-2222-2222-2222-222222222204', 'India', 'IN', 'Delhi', 'DL', 'East Delhi', 'East Delhi', '110092', 28.6358, 77.2965, true, NOW()),
('22222222-2222-2222-2222-222222222205', 'India', 'IN', 'Delhi', 'DL', 'West Delhi', 'West Delhi', '110015', 28.6467, 77.1490, true, NOW()),

-- KARNATAKA (KA)
('33333333-3333-3333-3333-333333333301', 'India', 'IN', 'Karnataka', 'KA', 'Bengaluru', 'Bengaluru Urban', '560001', 12.9716, 77.5946, true, NOW()),
('33333333-3333-3333-3333-333333333302', 'India', 'IN', 'Karnataka', 'KA', 'Mysuru', 'Mysuru', '570001', 12.2958, 76.6394, true, NOW()),
('33333333-3333-3333-3333-333333333303', 'India', 'IN', 'Karnataka', 'KA', 'Hubli', 'Dharwad', '580020', 15.3647, 75.1240, true, NOW()),
('33333333-3333-3333-3333-333333333304', 'India', 'IN', 'Karnataka', 'KA', 'Mangaluru', 'Dakshina Kannada', '575001', 12.9141, 74.8560, true, NOW()),
('33333333-3333-3333-3333-333333333305', 'India', 'IN', 'Karnataka', 'KA', 'Belagavi', 'Belagavi', '590001', 15.8497, 74.4977, true, NOW()),
('33333333-3333-3333-3333-333333333306', 'India', 'IN', 'Karnataka', 'KA', 'Gulbarga', 'Kalaburagi', '585101', 17.3297, 76.8343, true, NOW()),
('33333333-3333-3333-3333-333333333307', 'India', 'IN', 'Karnataka', 'KA', 'Davanagere', 'Davanagere', '577001', 14.4644, 75.9218, true, NOW()),

-- TAMIL NADU (TN)
('44444444-4444-4444-4444-444444444401', 'India', 'IN', 'Tamil Nadu', 'TN', 'Chennai', 'Chennai', '600001', 13.0827, 80.2707, true, NOW()),
('44444444-4444-4444-4444-444444444402', 'India', 'IN', 'Tamil Nadu', 'TN', 'Coimbatore', 'Coimbatore', '641001', 11.0168, 76.9558, true, NOW()),
('44444444-4444-4444-4444-444444444403', 'India', 'IN', 'Tamil Nadu', 'TN', 'Madurai', 'Madurai', '625001', 9.9252, 78.1198, true, NOW()),
('44444444-4444-4444-4444-444444444404', 'India', 'IN', 'Tamil Nadu', 'TN', 'Tiruchirappalli', 'Tiruchirappalli', '620001', 10.7905, 78.7047, true, NOW()),
('44444444-4444-4444-4444-444444444405', 'India', 'IN', 'Tamil Nadu', 'TN', 'Salem', 'Salem', '636001', 11.6643, 78.1460, true, NOW()),
('44444444-4444-4444-4444-444444444406', 'India', 'IN', 'Tamil Nadu', 'TN', 'Tirunelveli', 'Tirunelveli', '627001', 8.7139, 77.7567, true, NOW()),
('44444444-4444-4444-4444-444444444407', 'India', 'IN', 'Tamil Nadu', 'TN', 'Vellore', 'Vellore', '632001', 12.9165, 79.1325, true, NOW()),
('44444444-4444-4444-4444-444444444408', 'India', 'IN', 'Tamil Nadu', 'TN', 'Erode', 'Erode', '638001', 11.3410, 77.7172, true, NOW()),

-- UTTAR PRADESH (UP)
('55555555-5555-5555-5555-555555555501', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Lucknow', 'Lucknow', '226001', 26.8467, 80.9462, true, NOW()),
('55555555-5555-5555-5555-555555555502', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Kanpur', 'Kanpur Nagar', '208001', 26.4499, 80.3319, true, NOW()),
('55555555-5555-5555-5555-555555555503', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Agra', 'Agra', '282001', 27.1767, 78.0081, true, NOW()),
('55555555-5555-5555-5555-555555555504', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Varanasi', 'Varanasi', '221001', 25.3176, 82.9739, true, NOW()),
('55555555-5555-5555-5555-555555555505', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Prayagraj', 'Prayagraj', '211001', 25.4358, 81.8463, true, NOW()),
('55555555-5555-5555-5555-555555555506', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Ghaziabad', 'Ghaziabad', '201001', 28.6692, 77.4538, true, NOW()),
('55555555-5555-5555-5555-555555555507', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Noida', 'Gautam Buddha Nagar', '201301', 28.5355, 77.3910, true, NOW()),
('55555555-5555-5555-5555-555555555508', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Meerut', 'Meerut', '250001', 28.9845, 77.7064, true, NOW()),
('55555555-5555-5555-5555-555555555509', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Bareilly', 'Bareilly', '243001', 28.3670, 79.4304, true, NOW()),
('55555555-5555-5555-5555-555555555510', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Aligarh', 'Aligarh', '202001', 27.8974, 78.0880, true, NOW()),
('55555555-5555-5555-5555-555555555511', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Moradabad', 'Moradabad', '244001', 28.8387, 78.7738, true, NOW()),
('55555555-5555-5555-5555-555555555512', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Saharanpur', 'Saharanpur', '247001', 29.9679, 77.5450, true, NOW()),
('55555555-5555-5555-5555-555555555513', 'India', 'IN', 'Uttar Pradesh', 'UP', 'Gorakhpur', 'Gorakhpur', '273001', 26.7606, 83.3733, true, NOW()),

-- GUJARAT (GJ)
('66666666-6666-6666-6666-666666666601', 'India', 'IN', 'Gujarat', 'GJ', 'Ahmedabad', 'Ahmedabad', '380001', 23.0225, 72.5714, true, NOW()),
('66666666-6666-6666-6666-666666666602', 'India', 'IN', 'Gujarat', 'GJ', 'Surat', 'Surat', '395001', 21.1702, 72.8311, true, NOW()),
('66666666-6666-6666-6666-666666666603', 'India', 'IN', 'Gujarat', 'GJ', 'Vadodara', 'Vadodara', '390001', 22.3072, 73.1812, true, NOW()),
('66666666-6666-6666-6666-666666666604', 'India', 'IN', 'Gujarat', 'GJ', 'Rajkot', 'Rajkot', '360001', 22.3039, 70.8022, true, NOW()),
('66666666-6666-6666-6666-666666666605', 'India', 'IN', 'Gujarat', 'GJ', 'Bhavnagar', 'Bhavnagar', '364001', 21.7645, 72.1519, true, NOW()),
('66666666-6666-6666-6666-666666666606', 'India', 'IN', 'Gujarat', 'GJ', 'Jamnagar', 'Jamnagar', '361001', 22.4707, 70.0577, true, NOW()),
('66666666-6666-6666-6666-666666666607', 'India', 'IN', 'Gujarat', 'GJ', 'Gandhinagar', 'Gandhinagar', '382010', 23.2156, 72.6369, true, NOW()),
('66666666-6666-6666-6666-666666666608', 'India', 'IN', 'Gujarat', 'GJ', 'Gandhidham', 'Kutch', '370201', 23.0725, 70.1254, true, NOW()),

-- WEST BENGAL (WB)
('77777777-7777-7777-7777-777777777701', 'India', 'IN', 'West Bengal', 'WB', 'Kolkata', 'Kolkata', '700001', 22.5726, 88.3639, true, NOW()),
('77777777-7777-7777-7777-777777777702', 'India', 'IN', 'West Bengal', 'WB', 'Howrah', 'Howrah', '711101', 22.5958, 88.2636, true, NOW()),
('77777777-7777-7777-7777-777777777703', 'India', 'IN', 'West Bengal', 'WB', 'Durgapur', 'Paschim Bardhaman', '713201', 23.5204, 87.3119, true, NOW()),
('77777777-7777-7777-7777-777777777704', 'India', 'IN', 'West Bengal', 'WB', 'Asansol', 'Paschim Bardhaman', '713301', 23.6833, 86.9833, true, NOW()),
('77777777-7777-7777-7777-777777777705', 'India', 'IN', 'West Bengal', 'WB', 'Siliguri', 'Darjeeling', '734001', 26.7271, 88.3953, true, NOW()),
('77777777-7777-7777-7777-777777777706', 'India', 'IN', 'West Bengal', 'WB', 'Kharagpur', 'Paschim Medinipur', '721301', 22.3302, 87.3236, true, NOW()),

-- RAJASTHAN (RJ)
('88888888-8888-8888-8888-888888888801', 'India', 'IN', 'Rajasthan', 'RJ', 'Jaipur', 'Jaipur', '302001', 26.9124, 75.7873, true, NOW()),
('88888888-8888-8888-8888-888888888802', 'India', 'IN', 'Rajasthan', 'RJ', 'Jodhpur', 'Jodhpur', '342001', 26.2389, 73.0243, true, NOW()),
('88888888-8888-8888-8888-888888888803', 'India', 'IN', 'Rajasthan', 'RJ', 'Udaipur', 'Udaipur', '313001', 24.5854, 73.7125, true, NOW()),
('88888888-8888-8888-8888-888888888804', 'India', 'IN', 'Rajasthan', 'RJ', 'Kota', 'Kota', '324001', 25.2138, 75.8648, true, NOW()),
('88888888-8888-8888-8888-888888888805', 'India', 'IN', 'Rajasthan', 'RJ', 'Ajmer', 'Ajmer', '305001', 26.4499, 74.6399, true, NOW()),
('88888888-8888-8888-8888-888888888806', 'India', 'IN', 'Rajasthan', 'RJ', 'Bikaner', 'Bikaner', '334001', 28.0229, 73.3119, true, NOW()),
('88888888-8888-8888-8888-888888888807', 'India', 'IN', 'Rajasthan', 'RJ', 'Alwar', 'Alwar', '301001', 27.5667, 76.6167, true, NOW()),

-- MADHYA PRADESH (MP)
('99999999-9999-9999-9999-999999999901', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Bhopal', 'Bhopal', '462001', 23.2599, 77.4126, true, NOW()),
('99999999-9999-9999-9999-999999999902', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Indore', 'Indore', '452001', 22.7196, 75.8577, true, NOW()),
('99999999-9999-9999-9999-999999999903', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Jabalpur', 'Jabalpur', '482001', 23.1815, 79.9864, true, NOW()),
('99999999-9999-9999-9999-999999999904', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Gwalior', 'Gwalior', '474001', 26.2183, 78.1828, true, NOW()),
('99999999-9999-9999-9999-999999999905', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Ujjain', 'Ujjain', '456001', 23.1765, 75.7885, true, NOW()),
('99999999-9999-9999-9999-999999999906', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Sagar', 'Sagar', '470001', 23.8388, 78.7378, true, NOW()),
('99999999-9999-9999-9999-999999999907', 'India', 'IN', 'Madhya Pradesh', 'MP', 'Rewa', 'Rewa', '486001', 24.5324, 81.2920, true, NOW()),

-- PUNJAB (PB)
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa01', 'India', 'IN', 'Punjab', 'PB', 'Ludhiana', 'Ludhiana', '141001', 30.9010, 75.8573, true, NOW()),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa02', 'India', 'IN', 'Punjab', 'PB', 'Amritsar', 'Amritsar', '143001', 31.6340, 74.8723, true, NOW()),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa03', 'India', 'IN', 'Punjab', 'PB', 'Jalandhar', 'Jalandhar', '144001', 31.3260, 75.5762, true, NOW()),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa04', 'India', 'IN', 'Punjab', 'PB', 'Patiala', 'Patiala', '147001', 30.3398, 76.3869, true, NOW()),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa05', 'India', 'IN', 'Punjab', 'PB', 'Bathinda', 'Bathinda', '151001', 30.2110, 74.9455, true, NOW()),
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaa06', 'India', 'IN', 'Punjab', 'PB', 'Mohali', 'Sahibzada Ajit Singh Nagar', '160055', 30.7046, 76.7179, true, NOW()),

-- HARYANA (HR)
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb01', 'India', 'IN', 'Haryana', 'HR', 'Gurugram', 'Gurugram', '122001', 28.4595, 77.0266, true, NOW()),
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb02', 'India', 'IN', 'Haryana', 'HR', 'Faridabad', 'Faridabad', '121001', 28.4089, 77.3178, true, NOW()),
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb03', 'India', 'IN', 'Haryana', 'HR', 'Panipat', 'Panipat', '132103', 29.3904, 76.9635, true, NOW()),
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb04', 'India', 'IN', 'Haryana', 'HR', 'Ambala', 'Ambala', '134003', 30.3782, 76.7769, true, NOW()),
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbb05', 'India', 'IN', 'Haryana', 'HR', 'Hisar', 'Hisar', '125001', 29.1492, 75.7217, true, NOW()),

-- TELANGANA (TG)
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'India', 'IN', 'Telangana', 'TG', 'Hyderabad', 'Hyderabad', '500001', 17.3850, 78.4867, true, NOW()),
('cccccccc-cccc-cccc-cccc-cccccccccc02', 'India', 'IN', 'Telangana', 'TG', 'Warangal', 'Warangal', '506001', 17.9784, 79.5941, true, NOW()),
('cccccccc-cccc-cccc-cccc-cccccccccc03', 'India', 'IN', 'Telangana', 'TG', 'Nizamabad', 'Nizamabad', '503001', 18.6725, 78.0941, true, NOW()),

-- ANDHRA PRADESH (AP)
('dddddddd-dddd-dddd-dddd-dddddddddd01', 'India', 'IN', 'Andhra Pradesh', 'AP', 'Visakhapatnam', 'Visakhapatnam', '530001', 17.6868, 83.2185, true, NOW()),
('dddddddd-dddd-dddd-dddd-dddddddddd02', 'India', 'IN', 'Andhra Pradesh', 'AP', 'Vijayawada', 'Krishna', '520001', 16.5062, 80.6480, true, NOW()),
('dddddddd-dddd-dddd-dddd-dddddddddd03', 'India', 'IN', 'Andhra Pradesh', 'AP', 'Guntur', 'Guntur', '522001', 16.3067, 80.4365, true, NOW()),
('dddddddd-dddd-dddd-dddd-dddddddddd04', 'India', 'IN', 'Andhra Pradesh', 'AP', 'Tirupati', 'Chittoor', '517501', 13.6288, 79.4192, true, NOW()),

-- KERALA (KL)
('eeeeeeee-eeee-eeee-eeee-eeeeeeeeee01', 'India', 'IN', 'Kerala', 'KL', 'Thiruvananthapuram', 'Thiruvananthapuram', '695001', 8.5241, 76.9366, true, NOW()),
('eeeeeeee-eeee-eeee-eeee-eeeeeeeeee02', 'India', 'IN', 'Kerala', 'KL', 'Kochi', 'Ernakulam', '682001', 9.9312, 76.2673, true, NOW()),
('eeeeeeee-eeee-eeee-eeee-eeeeeeeeee03', 'India', 'IN', 'Kerala', 'KL', 'Kozhikode', 'Kozhikode', '673001', 11.2588, 75.7804, true, NOW()),
('eeeeeeee-eeee-eeee-eeee-eeeeeeeeee04', 'India', 'IN', 'Kerala', 'KL', 'Thrissur', 'Thrissur', '680001', 10.5276, 76.2144, true, NOW()),
('eeeeeeee-eeee-eeee-eeee-eeeeeeeeee05', 'India', 'IN', 'Kerala', 'KL', 'Kollam', 'Kollam', '691001', 8.8932, 76.6141, true, NOW()),

-- BIHAR (BR)
('ffffffff-ffff-ffff-ffff-ffffffffff01', 'India', 'IN', 'Bihar', 'BR', 'Patna', 'Patna', '800001', 25.5941, 85.1376, true, NOW()),
('ffffffff-ffff-ffff-ffff-ffffffffff02', 'India', 'IN', 'Bihar', 'BR', 'Gaya', 'Gaya', '823001', 24.7955, 84.9994, true, NOW()),
('ffffffff-ffff-ffff-ffff-ffffffffff03', 'India', 'IN', 'Bihar', 'BR', 'Bhagalpur', 'Bhagalpur', '812001', 25.2425, 86.9842, true, NOW()),
('ffffffff-ffff-ffff-ffff-ffffffffff04', 'India', 'IN', 'Bihar', 'BR', 'Muzaffarpur', 'Muzaffarpur', '842001', 26.1209, 85.3645, true, NOW()),

-- ODISHA (OD)
('11111111-aaaa-1111-aaaa-111111111101', 'India', 'IN', 'Odisha', 'OD', 'Bhubaneswar', 'Khordha', '751001', 20.2961, 85.8245, true, NOW()),
('11111111-aaaa-1111-aaaa-111111111102', 'India', 'IN', 'Odisha', 'OD', 'Cuttack', 'Cuttack', '753001', 20.4625, 85.8830, true, NOW()),
('11111111-aaaa-1111-aaaa-111111111103', 'India', 'IN', 'Odisha', 'OD', 'Rourkela', 'Sundargarh', '769001', 22.2604, 84.8536, true, NOW()),
('11111111-aaaa-1111-aaaa-111111111104', 'India', 'IN', 'Odisha', 'OD', 'Berhampur', 'Ganjam', '760001', 19.3150, 84.7942, true, NOW()),

-- CHHATTISGARH (CG)
('22222222-bbbb-2222-bbbb-222222222201', 'India', 'IN', 'Chhattisgarh', 'CG', 'Raipur', 'Raipur', '492001', 21.2514, 81.6296, true, NOW()),
('22222222-bbbb-2222-bbbb-222222222202', 'India', 'IN', 'Chhattisgarh', 'CG', 'Bhilai', 'Durg', '490001', 21.1938, 81.3509, true, NOW()),
('22222222-bbbb-2222-bbbb-222222222203', 'India', 'IN', 'Chhattisgarh', 'CG', 'Bilaspur', 'Bilaspur', '495001', 22.0797, 82.1409, true, NOW()),

-- JHARKHAND (JH)
('33333333-cccc-3333-cccc-333333333301', 'India', 'IN', 'Jharkhand', 'JH', 'Ranchi', 'Ranchi', '834001', 23.3441, 85.3096, true, NOW()),
('33333333-cccc-3333-cccc-333333333302', 'India', 'IN', 'Jharkhand', 'JH', 'Jamshedpur', 'East Singhbhum', '831001', 22.8046, 86.2029, true, NOW()),
('33333333-cccc-3333-cccc-333333333303', 'India', 'IN', 'Jharkhand', 'JH', 'Dhanbad', 'Dhanbad', '826001', 23.7957, 86.4304, true, NOW()),

-- UTTARAKHAND (UK)
('44444444-dddd-4444-dddd-444444444401', 'India', 'IN', 'Uttarakhand', 'UK', 'Dehradun', 'Dehradun', '248001', 30.3165, 78.0322, true, NOW()),
('44444444-dddd-4444-dddd-444444444402', 'India', 'IN', 'Uttarakhand', 'UK', 'Haridwar', 'Haridwar', '249401', 29.9457, 78.1642, true, NOW()),
('44444444-dddd-4444-dddd-444444444403', 'India', 'IN', 'Uttarakhand', 'UK', 'Roorkee', 'Haridwar', '247667', 29.8543, 77.8880, true, NOW()),

-- GOA (GA)
('55555555-eeee-5555-eeee-555555555501', 'India', 'IN', 'Goa', 'GA', 'Panaji', 'North Goa', '403001', 15.4909, 73.8278, true, NOW()),
('55555555-eeee-5555-eeee-555555555502', 'India', 'IN', 'Goa', 'GA', 'Margao', 'South Goa', '403601', 15.2759, 73.9548, true, NOW()),

-- HIMACHAL PRADESH (HP)
('66666666-ffff-6666-ffff-666666666601', 'India', 'IN', 'Himachal Pradesh', 'HP', 'Shimla', 'Shimla', '171001', 31.1048, 77.1734, true, NOW()),
('66666666-ffff-6666-ffff-666666666602', 'India', 'IN', 'Himachal Pradesh', 'HP', 'Manali', 'Kullu', '175131', 32.2432, 77.1897, true, NOW()),
('66666666-ffff-6666-ffff-666666666603', 'India', 'IN', 'Himachal Pradesh', 'HP', 'Dharamshala', 'Kangra', '176215', 32.2190, 76.3235, true, NOW()),

-- JAMMU & KASHMIR (JK)
('77777777-aaaa-7777-aaaa-777777777701', 'India', 'IN', 'Jammu & Kashmir', 'JK', 'Srinagar', 'Srinagar', '190001', 34.0837, 74.7973, true, NOW()),
('77777777-aaaa-7777-aaaa-777777777702', 'India', 'IN', 'Jammu & Kashmir', 'JK', 'Jammu', 'Jammu', '180001', 32.7266, 74.8570, true, NOW()),

-- ASSAM (AS)
('88888888-bbbb-8888-bbbb-888888888801', 'India', 'IN', 'Assam', 'AS', 'Guwahati', 'Kamrup Metropolitan', '781001', 26.1445, 91.7362, true, NOW()),
('88888888-bbbb-8888-bbbb-888888888802', 'India', 'IN', 'Assam', 'AS', 'Silchar', 'Cachar', '788001', 24.8273, 92.7969, true, NOW()),
('88888888-bbbb-8888-bbbb-888888888803', 'India', 'IN', 'Assam', 'AS', 'Dibrugarh', 'Dibrugarh', '786001', 27.4728, 94.9120, true, NOW()),

-- CHANDIGARH (CH)
('99999999-cccc-9999-cccc-999999999901', 'India', 'IN', 'Chandigarh', 'CH', 'Chandigarh', 'Chandigarh', '160001', 30.7333, 76.7794, true, NOW());

-- =============================================
-- INSERT CITY AREAS (Major Localities)
-- Using the GUID references
-- =============================================

-- Mumbai Areas (Location ID: 11111111-1111-1111-1111-111111111101)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('11111111-1111-1111-1111-111111111101', 'Andheri East', '400069', true),
('11111111-1111-1111-1111-111111111101', 'Andheri West', '400058', true),
('11111111-1111-1111-1111-111111111101', 'Bandra West', '400050', true),
('11111111-1111-1111-1111-111111111101', 'Bandra East', '400051', true),
('11111111-1111-1111-1111-111111111101', 'Juhu', '400049', true),
('11111111-1111-1111-1111-111111111101', 'Dadar', '400014', true),
('11111111-1111-1111-1111-111111111101', 'Worli', '400018', true),
('11111111-1111-1111-1111-111111111101', 'Powai', '400076', true),
('11111111-1111-1111-1111-111111111101', 'Goregaon', '400062', true),
('11111111-1111-1111-1111-111111111101', 'Malad', '400064', true),
('11111111-1111-1111-1111-111111111101', 'Borivali', '400066', true),
('11111111-1111-1111-1111-111111111101', 'Colaba', '400005', true),
('11111111-1111-1111-1111-111111111101', 'Nariman Point', '400021', true),
('11111111-1111-1111-1111-111111111101', 'Lower Parel', '400013', true),
('11111111-1111-1111-1111-111111111101', 'Chembur', '400071', true);

-- Pune Areas (Location ID: 11111111-1111-1111-1111-111111111102)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('11111111-1111-1111-1111-111111111102', 'Koregaon Park', '411001', true),
('11111111-1111-1111-1111-111111111102', 'Viman Nagar', '411014', true),
('11111111-1111-1111-1111-111111111102', 'Hinjewadi', '411057', true),
('11111111-1111-1111-1111-111111111102', 'Baner', '411045', true),
('11111111-1111-1111-1111-111111111102', 'Kharadi', '411014', true),
('11111111-1111-1111-1111-111111111102', 'Shivajinagar', '411005', true),
('11111111-1111-1111-1111-111111111102', 'Camp', '411001', true),
('11111111-1111-1111-1111-111111111102', 'Hadapsar', '411028', true),
('11111111-1111-1111-1111-111111111102', 'Pimple Saudagar', '411027', true),
('11111111-1111-1111-1111-111111111102', 'Wakad', '411057', true);

-- Bengaluru Areas (Location ID: 33333333-3333-3333-3333-333333333301)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('33333333-3333-3333-3333-333333333301', 'Indiranagar', '560038', true),
('33333333-3333-3333-3333-333333333301', 'Koramangala', '560034', true),
('33333333-3333-3333-3333-333333333301', 'Whitefield', '560066', true),
('33333333-3333-3333-3333-333333333301', 'Electronic City', '560100', true),
('33333333-3333-3333-3333-333333333301', 'Jayanagar', '560041', true),
('33333333-3333-3333-3333-333333333301', 'MG Road', '560001', true),
('33333333-3333-3333-3333-333333333301', 'Bellandur', '560103', true),
('33333333-3333-3333-3333-333333333301', 'HSR Layout', '560102', true),
('33333333-3333-3333-3333-333333333301', 'Marathahalli', '560037', true),
('33333333-3333-3333-3333-333333333301', 'BTM Layout', '560076', true),
('33333333-3333-3333-3333-333333333301', 'Rajajinagar', '560010', true),
('33333333-3333-3333-3333-333333333301', 'Yeshwanthpur', '560022', true);

-- Delhi Areas (Location ID: 22222222-2222-2222-2222-222222222201)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('22222222-2222-2222-2222-222222222201', 'Connaught Place', '110001', true),
('22222222-2222-2222-2222-222222222201', 'Chanakyapuri', '110021', true),
('22222222-2222-2222-2222-222222222201', 'South Extension', '110049', true),
('22222222-2222-2222-2222-222222222201', 'Greater Kailash', '110048', true),
('22222222-2222-2222-2222-222222222201', 'Lajpat Nagar', '110024', true),
('22222222-2222-2222-2222-222222222201', 'Karol Bagh', '110005', true),
('22222222-2222-2222-2222-222222222201', 'Rajouri Garden', '110027', true),
('22222222-2222-2222-2222-222222222201', 'Dwarka', '110075', true),
('22222222-2222-2222-2222-222222222201', 'Rohini', '110085', true),
('22222222-2222-2222-2222-222222222201', 'Pitampura', '110034', true),
('22222222-2222-2222-2222-222222222201', 'Saket', '110017', true),
('22222222-2222-2222-2222-222222222201', 'Vasant Kunj', '110070', true);

-- Chennai Areas (Location ID: 44444444-4444-4444-4444-444444444401)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('44444444-4444-4444-4444-444444444401', 'T Nagar', '600017', true),
('44444444-4444-4444-4444-444444444401', 'Anna Nagar', '600040', true),
('44444444-4444-4444-4444-444444444401', 'Adyar', '600020', true),
('44444444-4444-4444-4444-444444444401', 'Velachery', '600042', true),
('44444444-4444-4444-4444-444444444401', 'OMR', '600096', true),
('44444444-4444-4444-4444-444444444401', 'Porur', '600116', true),
('44444444-4444-4444-4444-444444444401', 'Tambaram', '600045', true),
('44444444-4444-4444-4444-444444444401', 'Guindy', '600032', true);

-- Hyderabad Areas (Location ID: cccccccc-cccc-cccc-cccc-cccccccccc01)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Gachibowli', '500032', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Hitech City', '500081', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Banjara Hills', '500034', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Jubilee Hills', '500033', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Madhapur', '500081', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Kondapur', '500084', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Secunderabad', '500003', true),
('cccccccc-cccc-cccc-cccc-cccccccccc01', 'Ameerpet', '500016', true);

-- Kolkata Areas (Location ID: 77777777-7777-7777-7777-777777777701)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('77777777-7777-7777-7777-777777777701', 'Park Street', '700016', true),
('77777777-7777-7777-7777-777777777701', 'Salt Lake City', '700091', true),
('77777777-7777-7777-7777-777777777701', 'New Town', '700156', true),
('77777777-7777-7777-7777-777777777701', 'Rajarhat', '700136', true),
('77777777-7777-7777-7777-777777777701', 'South City', '700068', true),
('77777777-7777-7777-7777-777777777701', 'Alipore', '700027', true),
('77777777-7777-7777-7777-777777777701', 'Ballygunge', '700019', true),
('77777777-7777-7777-7777-777777777701', 'Howrah', '711101', true);

-- Ahmedabad Areas (Location ID: 66666666-6666-6666-6666-666666666601)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('66666666-6666-6666-6666-666666666601', 'SG Highway', '380054', true),
('66666666-6666-6666-6666-666666666601', 'Prahlad Nagar', '380015', true),
('66666666-6666-6666-6666-666666666601', 'Vastrapur', '380015', true),
('66666666-6666-6666-6666-666666666601', 'Bodakdev', '380054', true),
('66666666-6666-6666-6666-666666666601', 'Satellite', '380015', true),
('66666666-6666-6666-6666-666666666601', 'Navrangpura', '380009', true);

-- Jaipur Areas (Location ID: 88888888-8888-8888-8888-888888888801)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('88888888-8888-8888-8888-888888888801', 'C Scheme', '302001', true),
('88888888-8888-8888-8888-888888888801', 'Vaishali Nagar', '302021', true),
('88888888-8888-8888-8888-888888888801', 'Malviya Nagar', '302017', true),
('88888888-8888-8888-8888-888888888801', 'Tonk Road', '302018', true),
('88888888-8888-8888-8888-888888888801', 'Jagatpura', '302017', true);

-- Lucknow Areas (Location ID: 55555555-5555-5555-5555-555555555501)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('55555555-5555-5555-5555-555555555501', 'Hazratganj', '226001', true),
('55555555-5555-5555-5555-555555555501', 'Gomti Nagar', '226010', true),
('55555555-5555-5555-5555-555555555501', 'Indira Nagar', '226016', true),
('55555555-5555-5555-5555-555555555501', 'Alambagh', '226005', true);

-- Surat Areas (Location ID: 66666666-6666-6666-6666-666666666602)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('66666666-6666-6666-6666-666666666602', 'City Light', '395007', true),
('66666666-6666-6666-6666-666666666602', 'Vesu', '395007', true),
('66666666-6666-6666-6666-666666666602', 'Adajan', '395009', true),
('66666666-6666-6666-6666-666666666602', 'Piplod', '395007', true);

-- Nagpur Areas (Location ID: 11111111-1111-1111-1111-111111111103)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('11111111-1111-1111-1111-111111111103', 'Dharampeth', '440010', true),
('11111111-1111-1111-1111-111111111103', 'Sadar', '440001', true),
('11111111-1111-1111-1111-111111111103', 'Civil Lines', '440001', true),
('11111111-1111-1111-1111-111111111103', 'Manish Nagar', '440015', true);

-- Indore Areas (Location ID: 99999999-9999-9999-9999-999999999902)
INSERT INTO city_areas (location_id, area_name, pincode, is_active) VALUES
('99999999-9999-9999-9999-999999999902', 'Vijay Nagar', '452010', true),
('99999999-9999-9999-9999-999999999902', 'Scheme No 54', '452010', true),
('99999999-9999-9999-9999-999999999902', 'Bhawarkua', '452001', true),
('99999999-9999-9999-9999-999999999902', 'Rajendra Nagar', '452012', true);

-- =============================================
-- CREATE INDEXES FOR PERFORMANCE
-- =============================================

-- Index for city searches
CREATE INDEX IF NOT EXISTS idx_locations_city ON locations(city_name);

-- Index for state searches
CREATE INDEX IF NOT EXISTS idx_locations_state ON locations(state_code);

-- Index for pincode searches
CREATE INDEX IF NOT EXISTS idx_locations_pincode ON locations(pincode);

-- Composite index for location lookups
CREATE INDEX IF NOT EXISTS idx_locations_state_city ON locations(state_code, city_name);

-- Index for area searches
CREATE INDEX IF NOT EXISTS idx_city_areas_location ON city_areas(location_id);
CREATE INDEX IF NOT EXISTS idx_city_areas_pincode ON city_areas(pincode);

-- GUID-specific indexes (for performance)
CREATE INDEX IF NOT EXISTS idx_locations_id ON locations(id);
CREATE INDEX IF NOT EXISTS idx_city_areas_location_id ON city_areas(location_id);

-- =============================================
-- CREATE USEFUL VIEWS (Optional)
-- =============================================

-- View for quick location search
CREATE OR REPLACE VIEW vw_location_search AS
SELECT 
    id,
    city_name,
    state_name,
    state_code,
    country_name,
    pincode,
    latitude,
    longitude
FROM locations
WHERE is_active = true;

-- View for city with areas (JSON aggregation)
CREATE OR REPLACE VIEW vw_city_with_areas AS
SELECT 
    l.id as location_id,
    l.city_name,
    l.state_name,
    l.state_code,
    json_agg(json_build_object('area_name', ca.area_name, 'pincode', ca.pincode)) as areas
FROM locations l
LEFT JOIN city_areas ca ON l.id = ca.location_id
WHERE l.is_active = true
GROUP BY l.id, l.city_name, l.state_name, l.state_code;

-- =============================================
-- VERIFY DATA
-- =============================================

-- Check total locations inserted
SELECT COUNT(*) as total_locations FROM locations;

-- Check locations by state
SELECT state_name, COUNT(*) as city_count 
FROM locations 
GROUP BY state_name 
ORDER BY city_count DESC;

-- Check areas inserted
SELECT COUNT(*) as total_areas FROM city_areas;

-- Sample query: Get all areas in Mumbai
SELECT l.city_name, ca.area_name, ca.pincode
FROM locations l
JOIN city_areas ca ON l.id = ca.location_id
WHERE l.city_name = 'Mumbai';

-- Sample query: Find location by GUID
SELECT * FROM locations WHERE id = '11111111-1111-1111-1111-111111111101';

-- =============================================
-- DONE!
-- =============================================