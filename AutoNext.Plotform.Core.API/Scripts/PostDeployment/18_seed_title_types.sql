-- =====================================================
-- Script 18: Seed Title Types Data
-- =====================================================

INSERT INTO public.title_types (name, code, description, is_clean, affects_value, value_deduction_percentage, display_order) VALUES
    ('Clean', 'CLEAN', 'No issues, never been in major accident', true, false, 0, 1),
    ('Salvage', 'SALVAGE', 'Vehicle declared total loss by insurance', false, true, 40, 2),
    ('Rebuilt', 'REBUILT', 'Previously salvage but repaired and inspected', false, true, 30, 3),
    ('Flood Damage', 'FLOOD', 'Vehicle has flood damage history', false, true, 50, 4),
    ('Hail Damage', 'HAIL', 'Vehicle has hail damage', false, true, 20, 5),
    ('Lemon Law', 'LEMON', 'Vehicle bought back under lemon law', false, true, 45, 6),
    ('Odometer Rollback', 'ROLLBACK', 'Odometer has been tampered with', false, true, 35, 7),
    ('Theft Recovery', 'THEFT', 'Vehicle was stolen and recovered', false, true, 25, 8),
    ('Fleet Use', 'FLEET', 'Former commercial/fleet vehicle', false, true, 15, 9),
    ('Rental Use', 'RENTAL', 'Former rental vehicle', false, true, 15, 10),
    ('Manufacturer Buyback', 'BUYBACK', 'Vehicle bought back by manufacturer', false, true, 30, 11),
    ('Clear', 'CLEAR', 'Clear title with no liens', true, false, 0, 12);

SELECT 'Title types seeded successfully' as Status;