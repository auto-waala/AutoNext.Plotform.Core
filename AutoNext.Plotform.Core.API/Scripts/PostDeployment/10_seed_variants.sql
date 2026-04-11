-- =====================================================
-- Script 10: Seed Vehicle Variants Data (Sample)
-- =====================================================

-- Insert variants for Toyota Camry
INSERT INTO public.vehicle_variants (model_id, name, code, description, fuel_type_id, transmission_id, drive_type, engine_size, horsepower, seating_capacity, base_price, display_order)
SELECT 
    m.id,
    v.name,
    v.code,
    v.description,
    ft.id,
    t.id,
    v.drive_type,
    v.engine_size,
    v.horsepower,
    v.seating_capacity,
    v.base_price,
    v.display_order
FROM models m
CROSS JOIN (VALUES 
    ('LE', 'LE', 'Base model with essential features', 'PETROL', 'AT8', 'FWD', 2.5, 203, 5, 25000.00, 1),
    ('SE', 'SE', 'Sporty appearance and features', 'PETROL', 'AT8', 'FWD', 2.5, 203, 5, 27000.00, 2),
    ('XLE', 'XLE', 'Luxury-focused trim', 'PETROL', 'AT8', 'FWD', 2.5, 203, 5, 30000.00, 3),
    ('XSE', 'XSE', 'Sport-luxury combination', 'PETROL', 'AT8', 'FWD', 2.5, 203, 5, 32000.00, 4),
    ('TRD', 'TRD', 'Performance-oriented trim', 'PETROL', 'AT8', 'FWD', 3.5, 301, 5, 33000.00, 5),
    ('Hybrid LE', 'HYB_LE', 'Hybrid base model', 'HYBRID', 'CVT', 'FWD', 2.5, 208, 5, 28000.00, 6),
    ('Hybrid XLE', 'HYB_XLE', 'Hybrid luxury trim', 'HYBRID', 'CVT', 'FWD', 2.5, 208, 5, 33000.00, 7)
) AS v(name, code, description, fuel_code, trans_code, drive_type, engine_size, horsepower, seating_capacity, base_price, display_order)
LEFT JOIN fuel_types ft ON ft.code = v.fuel_code
LEFT JOIN transmissions t ON t.code = v.trans_code
WHERE m.name = 'Camry' AND m.brand_id = (SELECT id FROM brands WHERE code = 'TOYOTA');

-- Insert variants for Honda Civic
INSERT INTO public.vehicle_variants (model_id, name, code, description, fuel_type_id, transmission_id, drive_type, engine_size, horsepower, seating_capacity, base_price, display_order)
SELECT 
    m.id,
    v.name,
    v.code,
    v.description,
    ft.id,
    t.id,
    v.drive_type,
    v.engine_size,
    v.horsepower,
    v.seating_capacity,
    v.base_price,
    v.display_order
FROM models m
CROSS JOIN (VALUES 
    ('LX', 'LX', 'Base model', 'PETROL', 'CVT', 'FWD', 2.0, 158, 5, 22000.00, 1),
    ('Sport', 'SPORT', 'Sporty appearance', 'PETROL', 'CVT', 'FWD', 2.0, 158, 5, 24000.00, 2),
    ('EX', 'EX', 'Enhanced features', 'PETROL', 'CVT', 'FWD', 1.5, 180, 5, 26000.00, 3),
    ('Touring', 'TOURING', 'Top of the line', 'PETROL', 'CVT', 'FWD', 1.5, 180, 5, 29000.00, 4),
    ('Si', 'SI', 'Performance-oriented', 'PETROL', 'MT6', 'FWD', 1.5, 200, 5, 28000.00, 5),
    ('Type R', 'TYPE_R', 'High-performance', 'PETROL', 'MT6', 'FWD', 2.0, 306, 4, 38000.00, 6)
) AS v(name, code, description, fuel_code, trans_code, drive_type, engine_size, horsepower, seating_capacity, base_price, display_order)
LEFT JOIN fuel_types ft ON ft.code = v.fuel_code
LEFT JOIN transmissions t ON t.code = v.trans_code
WHERE m.name = 'Civic' AND m.brand_id = (SELECT id FROM brands WHERE code = 'HONDA');

-- Verify seed data
SELECT 
    b.name as brand,
    m.name as model,
    v.name as variant,
    v.engine_size,
    v.horsepower,
    ft.name as fuel,
    t.name as transmission,
    v.base_price
FROM public.vehicle_variants v
JOIN public.models m ON v.model_id = m.id
JOIN public.brands b ON m.brand_id = b.id
LEFT JOIN public.fuel_types ft ON v.fuel_type_id = ft.id
LEFT JOIN public.transmissions t ON v.transmission_id = t.id
ORDER BY b.name, m.name, v.display_order;