-- =====================================================
-- Script 3: Seed Fuel Types Data
-- =====================================================

-- Insert fuel types
INSERT INTO public.fuel_types (name, code, description, icon_url, applicable_categories, display_order, is_active) VALUES
    ('Petrol', 'PETROL', 'Gasoline/petrol engine - spark-ignition internal combustion engine', '/icons/fuel/petrol.svg', '["CARS", "BIKES", "TRUCKS", "SUV", "VANS", "SCOOTERS"]', 1, true),
    ('Diesel', 'DIESEL', 'Compression-ignition engine, known for fuel efficiency and torque', '/icons/fuel/diesel.svg', '["CARS", "TRUCKS", "SUV", "VANS", "HEAVY"]', 2, true),
    ('Electric', 'ELECTRIC', 'Battery electric vehicle - zero emissions', '/icons/fuel/electric.svg', '["CARS", "BIKES", "TRUCKS", "SUV", "VANS", "SCOOTERS", "EV", "CYCLES"]', 3, true),
    ('Hybrid', 'HYBRID', 'Combines petrol engine with electric motor', '/icons/fuel/hybrid.svg', '["CARS", "SUV", "VANS"]', 4, true),
    ('Plug-in Hybrid', 'PHEV', 'Plug-in hybrid electric vehicle - can be charged externally', '/icons/fuel/phev.svg', '["CARS", "SUV"]', 5, true),
    ('CNG', 'CNG', 'Compressed Natural Gas - cleaner alternative fuel', '/icons/fuel/cng.svg', '["CARS", "TRUCKS", "VANS"]', 6, true),
    ('LPG', 'LPG', 'Liquefied Petroleum Gas - alternative fuel', '/icons/fuel/lpg.svg', '["CARS", "TRUCKS"]', 7, true),
    ('Hydrogen', 'HYDROGEN', 'Hydrogen fuel cell vehicle', '/icons/fuel/hydrogen.svg', '["CARS", "TRUCKS", "HEAVY"]', 8, true),
    ('Ethanol', 'ETHANOL', 'Flex-fuel vehicle running on ethanol blends', '/icons/fuel/ethanol.svg', '["CARS"]', 9, true),
    ('Bio Diesel', 'BIODSL', 'Biodiesel - renewable fuel from biological sources', '/icons/fuel/biodiesel.svg', '["TRUCKS", "HEAVY"]', 10, true);

-- Verify seed data
SELECT name, code, applicable_categories, display_order FROM public.fuel_types ORDER BY display_order;