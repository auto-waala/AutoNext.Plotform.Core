-- =====================================================
-- Script 17: Seed Warranty Types Data
-- =====================================================

INSERT INTO public.warranty_types (name, code, description, duration_months, duration_km, is_transferable, applicable_categories, display_order) VALUES
    ('Standard Manufacturer', 'STD_MFG', 'Standard manufacturer warranty', 36, 60000, true, '["CARS", "SUV", "TRUCKS"]', 1),
    ('Extended Warranty', 'EXTENDED', 'Extended warranty coverage', 60, 100000, true, '["CARS", "SUV", "TRUCKS", "BIKES"]', 2),
    ('Powertrain Only', 'POWERTRAIN', 'Covers engine and transmission only', 60, 80000, true, '["CARS", "SUV", "TRUCKS"]', 3),
    ('Bumper to Bumper', 'BUMPER', 'Comprehensive coverage', 36, 50000, true, '["CARS", "SUV", "LUXURY"]', 4),
    ('Limited Warranty', 'LIMITED', 'Limited coverage on specific components', 12, 20000, false, '["CARS", "BIKES", "TRUCKS"]', 5),
    ('Certified Pre-Owned', 'CPO', 'Manufacturer certified pre-owned warranty', 12, 20000, true, '["CARS", "SUV"]', 6),
    ('As-Is', 'ASIS', 'No warranty, sold as-is', 0, 0, false, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 7),
    ('Battery Warranty', 'BATTERY', 'Electric vehicle battery warranty', 96, 160000, true, '["EV", "ELECTRIC"]', 8),
    ('Frame Warranty', 'FRAME', 'Lifetime frame warranty for bicycles', 120, 0, true, '["CYCLES"]', 9),
    ('Dealer Warranty', 'DEALER', 'Dealer provided warranty', 24, 30000, false, '["CARS", "BIKES", "TRUCKS"]', 10);

SELECT 'Warranty types seeded successfully' as Status;