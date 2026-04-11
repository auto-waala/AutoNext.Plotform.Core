-- =====================================================
-- Script 4: Seed Transmissions Data
-- =====================================================

-- Insert transmission types
INSERT INTO public.transmissions (name, code, description, transmission_type, gears_count, applicable_categories, display_order, is_active) VALUES
    -- Manual Transmissions
    ('Manual 5-Speed', 'MT5', 'Manual transmission with 5 forward gears', 'Manual', 5, '["CARS", "BIKES", "TRUCKS", "SUV", "VANS"]', 1, true),
    ('Manual 6-Speed', 'MT6', 'Manual transmission with 6 forward gears', 'Manual', 6, '["CARS", "BIKES", "TRUCKS", "SUV", "SPORTS"]', 2, true),
    ('Manual 7-Speed', 'MT7', 'Manual transmission with 7 forward gears', 'Manual', 7, '["CARS", "SPORTS"]', 3, true),
    
    -- Automatic Transmissions
    ('Automatic 4-Speed', 'AT4', 'Automatic transmission with 4 speeds', 'Automatic', 4, '["CARS", "TRUCKS", "SUV", "VANS"]', 4, true),
    ('Automatic 5-Speed', 'AT5', 'Automatic transmission with 5 speeds', 'Automatic', 5, '["CARS", "SUV"]', 5, true),
    ('Automatic 6-Speed', 'AT6', 'Automatic transmission with 6 speeds', 'Automatic', 6, '["CARS", "TRUCKS", "SUV"]', 6, true),
    ('Automatic 7-Speed', 'AT7', 'Automatic transmission with 7 speeds', 'Automatic', 7, '["CARS", "SUV", "LUXURY"]', 7, true),
    ('Automatic 8-Speed', 'AT8', 'Automatic transmission with 8 speeds', 'Automatic', 8, '["CARS", "SUV", "LUXURY"]', 8, true),
    ('Automatic 9-Speed', 'AT9', 'Automatic transmission with 9 speeds', 'Automatic', 9, '["CARS", "LUXURY"]', 9, true),
    ('Automatic 10-Speed', 'AT10', 'Automatic transmission with 10 speeds', 'Automatic', 10, '["CARS", "SPORTS", "LUXURY"]', 10, true),
    
    -- CVT Transmissions
    ('CVT', 'CVT', 'Continuously Variable Transmission - seamless gear ratios', 'CVT', NULL, '["CARS", "SUV", "VANS", "SCOOTERS"]', 11, true),
    
    -- DCT Transmissions
    ('DCT', 'DCT', 'Dual-Clutch Transmission - rapid gear shifts', 'DCT', NULL, '["CARS", "SPORTS", "LUXURY"]', 12, true),
    
    -- Other Transmissions
    ('AMT', 'AMT', 'Automated Manual Transmission - clutch-less manual', 'Automated Manual', NULL, '["CARS", "TRUCKS"]', 13, true),
    ('Semi-Automatic', 'SEMI', 'Semi-automatic transmission with paddle shifters', 'Semi-Automatic', NULL, '["CARS", "SPORTS"]', 14, true),
    ('Electric Single Speed', 'EVT', 'Single-speed transmission for electric vehicles', 'Electric', 1, '["CARS", "BIKES", "EV", "SCOOTERS"]', 15, true),
    
    -- Motorcycle specific
    ('Chain Drive', 'CHAIN', 'Chain drive transmission', 'Chain', NULL, '["BIKES", "CYCLES"]', 16, true),
    ('Belt Drive', 'BELT', 'Belt drive transmission', 'Belt', NULL, '["BIKES", "CYCLES"]', 17, true),
    ('Shaft Drive', 'SHAFT', 'Shaft drive transmission', 'Shaft', NULL, '["BIKES"]', 18, true),
    
    -- Bicycle specific
    ('Derailleur Gears', 'DERAILLEUR', 'External gear system', 'Derailleur', NULL, '["CYCLES"]', 19, true),
    ('Hub Gears', 'HUB', 'Internal hub gear system', 'Hub', NULL, '["CYCLES"]', 20, true),
    ('Single Speed', 'SINGLE', 'Single speed, no gear shifting', 'Single Speed', 1, '["CYCLES"]', 21, true);

-- Verify seed data
SELECT name, code, transmission_type, gears_count, applicable_categories 
FROM public.transmissions 
ORDER BY display_order;