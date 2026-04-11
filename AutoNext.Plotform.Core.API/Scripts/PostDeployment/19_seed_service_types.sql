-- =====================================================
-- Script 19: Seed Service Types Data
-- =====================================================

INSERT INTO public.service_types (name, code, category, interval_months, interval_km, description, display_order) VALUES
    -- Routine Maintenance
    ('Oil Change', 'OIL_CHANGE', 'Routine', 6, 8000, 'Engine oil and filter replacement', 1),
    ('Tire Rotation', 'TIRE_ROTATE', 'Routine', 6, 10000, 'Rotate tires for even wear', 2),
    ('Air Filter', 'AIR_FILTER', 'Routine', 12, 20000, 'Replace engine air filter', 3),
    ('Cabin Filter', 'CABIN_FILTER', 'Routine', 12, 20000, 'Replace cabin air filter', 4),
    ('Brake Pads', 'BRAKE_PADS', 'Maintenance', 24, 40000, 'Replace brake pads', 5),
    ('Brake Fluid', 'BRAKE_FLUID', 'Maintenance', 24, 40000, 'Brake fluid flush and replace', 6),
    ('Coolant Flush', 'COOLANT', 'Maintenance', 36, 60000, 'Engine coolant replacement', 7),
    ('Transmission Fluid', 'TRANS_FLUID', 'Maintenance', 48, 80000, 'Transmission fluid change', 8),
    
    -- Major Services
    ('Timing Belt', 'TIMING_BELT', 'Repair', 96, 160000, 'Replace timing belt', 9),
    ('Spark Plugs', 'SPARK_PLUGS', 'Maintenance', 48, 80000, 'Replace spark plugs', 10),
    ('Fuel Filter', 'FUEL_FILTER', 'Maintenance', 36, 60000, 'Replace fuel filter', 11),
    
    -- Repairs
    ('Engine Repair', 'ENGINE_REPAIR', 'Repair', NULL, NULL, 'Major engine work', 12),
    ('Transmission Repair', 'TRANS_REPAIR', 'Repair', NULL, NULL, 'Transmission rebuild or repair', 13),
    ('AC Service', 'AC_SERVICE', 'Repair', 24, 40000, 'Air conditioning service', 14),
    ('Suspension Work', 'SUSPENSION', 'Repair', NULL, NULL, 'Suspension repair or replacement', 15),
    
    -- Inspections
    ('State Inspection', 'STATE_INSP', 'Inspection', 12, NULL, 'Required state safety inspection', 16),
    ('Emissions Test', 'EMISSIONS', 'Inspection', 12, NULL, 'Emissions testing', 17),
    ('Pre-purchase Inspection', 'PPI', 'Inspection', NULL, NULL, 'Independent pre-purchase inspection', 18),
    
    -- Bicycle Specific
    ('Tune-up', 'TUNEUP', 'Routine', 6, 500, 'Basic bicycle tune-up', 19),
    ('Brake Adjustment', 'BRAKE_ADJUST', 'Maintenance', 3, 200, 'Adjust bicycle brakes', 20),
    ('Gear Adjustment', 'GEAR_ADJUST', 'Maintenance', 3, 200, 'Adjust bicycle gears', 21);

SELECT 'Service types seeded successfully' as Status;