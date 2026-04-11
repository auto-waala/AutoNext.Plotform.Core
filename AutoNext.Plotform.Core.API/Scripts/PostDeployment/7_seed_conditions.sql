-- =====================================================
-- Script 7: Seed Vehicle Conditions Data
-- =====================================================

-- Insert vehicle conditions
INSERT INTO public.vehicle_conditions (name, code, description, display_order, is_active) VALUES
    ('Brand New', 'NEW', 'Never registered, 0 km/miles, full warranty', 1, true),
    ('Like New', 'LIKE_NEW', 'Almost new condition, very low mileage, no defects', 2, true),
    ('Excellent', 'EXCELLENT', 'Well-maintained, minor wear and tear, clean history', 3, true),
    ('Very Good', 'VERY_GOOD', 'Some visible wear, fully functional, well-maintained', 4, true),
    ('Good', 'GOOD', 'Normal wear for age, mechanically sound, may need cosmetic work', 5, true),
    ('Fair', 'FAIR', 'Noticeable issues, functional but may need repairs', 6, true),
    ('Needs Work', 'NEEDS_WORK', 'Requires repairs, sold as-is', 7, true),
    ('Refurbished', 'REFURBISHED', 'Professionally restored/repaired to good condition', 8, true),
    ('Certified Pre-Owned', 'CPO', 'Manufacturer certified, inspected, warranty included', 9, true),
    ('Salvage', 'SALVAGE', 'Previously damaged and rebuilt, salvage title', 10, true),
    ('Parts Only', 'PARTS', 'Not functional, for parts only', 11, false);

-- Verify seed data
SELECT name, code, description FROM public.vehicle_conditions ORDER BY display_order;