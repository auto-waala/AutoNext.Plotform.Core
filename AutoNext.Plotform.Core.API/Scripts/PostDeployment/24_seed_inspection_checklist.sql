-- =====================================================
-- Script 24: Seed Inspection Checklist Data
-- =====================================================

-- Generic inspection items (applicable to all motor vehicles)
INSERT INTO public.inspection_checklist (name, code, category, is_critical, weightage, display_order) VALUES
    -- Engine Category
    ('Engine Starts',           'ENGINE_START',     'Engine',       true,  10, 1),
    ('No Engine Knocking',      'NO_KNOCKING',      'Engine',       true,  10, 2),
    ('No Oil Leaks',            'OIL_LEAKS',        'Engine',       true,  9,  3),
    ('Check Engine Light Off',  'NO_CHECK_ENGINE',  'Engine',       true,  9,  4),
    ('Smooth Idle',             'SMOOTH_IDLE',      'Engine',       false, 7,  5),
    ('No Excessive Smoke',      'NO_SMOKE',         'Engine',       true,  8,  6),

    -- Transmission Category
    ('Smooth Gear Shifts',      'SMOOTH_SHIFTS',    'Transmission', true,  9,  7),
    ('No Transmission Leaks',   'TRANS_LEAKS',      'Transmission', true,  9,  8),
    ('Clutch Condition',        'CLUTCH_CONDITION', 'Transmission', false, 8,  9),

    -- Brakes Category
    ('Brake Pedal Feel',        'BRAKE_FEEL',       'Brakes',       true,  10, 10),
    ('Brake Pad Life',          'BRAKE_PADS',       'Brakes',       false, 7,  11),
    ('No Brake Noise',          'NO_BRAKE_NOISE',   'Brakes',       false, 6,  12),
    ('Parking Brake Works',     'PARKING_BRAKE',    'Brakes',       true,  8,  13),

    -- Exterior Category
    ('No Major Dents',          'NO_DENTS',         'Exterior',     false, 6,  14),
    ('Paint Condition',         'PAINT_CONDITION',  'Exterior',     false, 5,  15),
    ('No Rust',                 'NO_RUST',          'Exterior',     true,  8,  16),
    ('Glass Condition',         'GLASS_CONDITION',  'Exterior',     true,  7,  17),
    ('Tire Tread Depth',        'TIRE_TREAD',       'Exterior',     true,  8,  18),
    ('All Lights Working',      'LIGHTS_WORKING',   'Exterior',     true,  8,  19),

    -- Interior Category
    ('Seat Condition',          'SEAT_CONDITION',   'Interior',     false, 5,  20),
    ('Carpet Condition',        'CARPET_CONDITION', 'Interior',     false, 4,  21),
    ('AC/Heater Working',       'AC_WORKING',       'Interior',     true,  8,  22),
    ('All Gauges Working',      'GAUGES_WORKING',   'Interior',     true,  8,  23),
    ('No Warning Lights',       'NO_WARNING_LIGHTS','Interior',     true,  9,  24),
    ('Power Windows Working',   'PWR_WINDOWS',      'Interior',     false, 6,  25),
    ('Audio System Working',    'AUDIO_WORKING',    'Interior',     false, 5,  26),

    -- Electrical Category
    ('Battery Condition',       'BATTERY_CONDITION','Electrical',   true,  8,  27),
    ('Alternator Charging',     'ALTERNATOR',       'Electrical',   true,  8,  28),
    ('All Electronics Working', 'ELECTRONICS',      'Electrical',   false, 7,  29),

    -- Suspension Category
    ('No Suspension Noise',     'NO_SUSPENSION_NOISE','Suspension', true,  8,  30),
    ('Smooth Ride',             'SMOOTH_RIDE',      'Suspension',   false, 7,  31),
    ('Alignment Good',          'ALIGNMENT',        'Suspension',   false, 6,  32);

-- Motorcycle specific inspection items
INSERT INTO public.inspection_checklist (name, code, category, is_critical, weightage, applicable_vehicle_types, display_order) VALUES
    ('Chain Condition',         'CHAIN_CONDITION',  'Drivetrain',   true,  8,  '["BIKES"]', 33),
    ('Fork Seals No Leaks',     'FORK_SEALS',       'Suspension',   true,  8,  '["BIKES"]', 34),
    ('Tire Pressure OK',        'TIRE_PRESSURE',    'Wheels',       true,  7,  '["BIKES"]', 35),
    ('Mirrors Condition',       'MIRRORS',          'Safety',       false, 5,  '["BIKES"]', 36),
    ('Kickstand Function',      'KICKSTAND',        'Safety',       true,  7,  '["BIKES"]', 37);

-- Bicycle specific inspection items
INSERT INTO public.inspection_checklist (name, code, category, is_critical, weightage, applicable_vehicle_types, display_order) VALUES
    ('Frame No Cracks',         'FRAME_CRACKS',     'Frame',        true,  10, '["CYCLES"]', 38),
    ('Wheels True',             'WHEELS_TRUE',      'Wheels',       true,  8,  '["CYCLES"]', 39),
    ('Brake Cables Intact',     'BRAKE_CABLES',     'Brakes',       true,  9,  '["CYCLES"]', 40),
    ('Chain Lubricated',        'CHAIN_LUBE',       'Drivetrain',   false, 7,  '["CYCLES"]', 41),
    ('Derailleur Shifting OK',  'DERAILLEUR',       'Drivetrain',   false, 7,  '["CYCLES"]', 42),
    ('Handlebar Tight',         'HANDLEBAR',        'Safety',       true,  9,  '["CYCLES"]', 43),
    ('Saddle Secure',           'SADDLE',           'Safety',       true,  8,  '["CYCLES"]', 44),
    ('Pedals Condition',        'PEDALS',           'Drivetrain',   false, 6,  '["CYCLES"]', 45),
    ('Reflectors Present',      'REFLECTORS',       'Safety',       true,  7,  '["CYCLES"]', 46),
    ('Bell Working',            'BELL',             'Safety',       false, 4,  '["CYCLES"]', 47);

-- EV specific inspection items
INSERT INTO public.inspection_checklist (name, code, category, is_critical, weightage, applicable_vehicle_types, display_order) VALUES
    ('Battery Health %',        'BATTERY_HEALTH',   'Electrical',   true,  10, '["EV"]',    48),
    ('Charging Port Condition', 'CHARGE_PORT',      'Electrical',   true,  9,  '["EV"]',    49),
    ('Range Accuracy',          'RANGE_ACCURACY',   'Electrical',   false, 8,  '["EV"]',    50),
    ('Regenerative Braking OK', 'REGEN_BRAKING',    'Brakes',       false, 7,  '["EV"]',    51),
    ('Software Up To Date',     'SOFTWARE_UPDATE',  'Electrical',   false, 6,  '["EV"]',    52),
    ('Motor No Noise',          'MOTOR_NOISE',      'Engine',       true,  9,  '["EV"]',    53),
    ('Thermal Management OK',   'THERMAL_MGMT',     'Engine',       true,  9,  '["EV"]',    54);

-- Verify
SELECT
    category,
    COUNT(*)        AS total_items,
    SUM(CASE WHEN is_critical THEN 1 ELSE 0 END) AS critical_items
FROM public.inspection_checklist
GROUP BY category
ORDER BY category;