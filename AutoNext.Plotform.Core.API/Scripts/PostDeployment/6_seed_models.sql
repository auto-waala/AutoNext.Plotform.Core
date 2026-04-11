-- =====================================================
-- Script 6: Seed Models Data
-- =====================================================

-- Toyota Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Camry',        'CAMRY',       'camry',        'Mid-size sedan',                'SEDAN',   1983, NULL::INTEGER, true,  1),
    ('Corolla',      'COROLLA',     'corolla',      'Compact sedan',                 'SEDAN',   1966, NULL::INTEGER, true,  2),
    ('Yaris',        'YARIS',       'yaris',        'Subcompact car',                'HATCH',   1999, 2020,          false, 3),
    ('Avalon',       'AVALON',      'avalon',       'Full-size sedan',               'SEDAN',   1994, 2022,          false, 4),
    ('Prius',        'PRIUS',       'prius',        'Hybrid sedan',                  'SEDAN',   1997, NULL::INTEGER, true,  5),
    ('RAV4',         'RAV4',        'rav4',         'Compact SUV',                   'SUV',     1994, NULL::INTEGER, true,  6),
    ('Highlander',   'HIGHLANDER',  'highlander',   'Mid-size SUV',                  'SUV',     2000, NULL::INTEGER, true,  7),
    ('4Runner',      '4RUNNER',     '4runner',      'Mid-size body-on-frame SUV',    'SUV',     1984, NULL::INTEGER, true,  8),
    ('Land Cruiser', 'LANDCRUISER', 'land-cruiser', 'Full-size luxury SUV',          'SUV',     1951, 2021,          false, 9),
    ('Sequoia',      'SEQUOIA',     'sequoia',      'Full-size SUV',                 'SUV',     2000, NULL::INTEGER, true,  10),
    ('C-HR',         'CHR',         'chr',          'Subcompact crossover',          'CROSS',   2016, NULL::INTEGER, true,  11),
    ('Tacoma',       'TACOMA',      'tacoma',       'Mid-size pickup truck',         'PICKUP',  1995, NULL::INTEGER, true,  12),
    ('Tundra',       'TUNDRA',      'tundra',       'Full-size pickup truck',        'PICKUP',  1999, NULL::INTEGER, true,  13),
    ('Hilux',        'HILUX',       'hilux',        'Mid-size pickup (international)','PICKUP', 1968, NULL::INTEGER, true,  14),
    ('Sienna',       'SIENNA',      'sienna',       'Minivan',                       'MINIVAN', 1997, NULL::INTEGER, true,  15),
    ('Fortuner',     'FORTUNER',    'fortuner',     'Mid-size body-on-frame SUV',    'SUV',     2004, NULL::INTEGER, true,  16),
    ('Innova',       'INNOVA',      'innova',       'MPV / People carrier',          'MINIVAN', 2004, NULL::INTEGER, true,  17),
    ('GR Supra',     'SUPRA',       'gr-supra',     'Sports coupe',                  'SPORTS',  1978, NULL::INTEGER, true,  18),
    ('bZ4X',         'BZ4X',        'bz4x',         'All-electric SUV',              'SUV',     2022, NULL::INTEGER, true,  19)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'TOYOTA';

-- Honda Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Civic',       'CIVIC',      'civic',       'Compact car',              'SEDAN',      1972, NULL::INTEGER, true,  1),
    ('Accord',      'ACCORD',     'accord',      'Mid-size sedan',           'SEDAN',      1976, NULL::INTEGER, true,  2),
    ('City',        'CITY',       'city',        'Subcompact sedan',         'SEDAN',      1981, NULL::INTEGER, true,  3),
    ('Fit/Jazz',    'FIT',        'fit',         'Subcompact hatchback',     'HATCH',      2001, 2020,          false, 4),
    ('CR-V',        'CRV',        'cr-v',        'Compact SUV',              'SUV',        1995, NULL::INTEGER, true,  5),
    ('HR-V',        'HRV',        'hr-v',        'Subcompact crossover',     'CROSS',      1998, NULL::INTEGER, true,  6),
    ('Pilot',       'PILOT',      'pilot',       'Mid-size SUV',             'SUV',        2002, NULL::INTEGER, true,  7),
    ('Passport',    'PASSPORT',   'passport',    'Mid-size SUV',             'SUV',        1993, NULL::INTEGER, true,  8),
    ('WR-V',        'WRV',        'wr-v',        'Subcompact crossover SUV', 'CROSS',      2017, NULL::INTEGER, true,  9),
    ('Elevate',     'ELEVATE',    'elevate',     'Compact SUV for India',    'SUV',        2023, NULL::INTEGER, true,  10),
    ('CBR600RR',    'CBR600',     'cbr600rr',    'Sport motorcycle',         'SPORT_BIKE', 2003, NULL::INTEGER, true,  11),
    ('Gold Wing',   'GOLDWING',   'gold-wing',   'Touring motorcycle',       'TOURING',    1975, NULL::INTEGER, true,  12),
    ('CRF450R',     'CRF450',     'crf450r',     'Motocross bike',           'DIRT_BIKE',  2002, NULL::INTEGER, true,  13),
    ('Africa Twin', 'AFRICATWIN', 'africa-twin', 'Adventure motorcycle',     'ADVENTURE',  1988, NULL::INTEGER, true,  14),
    ('Activa',      'ACTIVA',     'activa',      'Automatic scooter',        'SCOOTER',    2001, NULL::INTEGER, true,  15),
    ('Shine',       'SHINE',      'shine',       'Commuter motorcycle',      'COMMUTER',   2006, NULL::INTEGER, true,  16)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'HONDA';

-- BMW Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('3 Series', '3SERIES', '3-series', 'Compact executive car',              'SEDAN',      1975, NULL::INTEGER, true, 1),
    ('5 Series', '5SERIES', '5-series', 'Executive car',                      'SEDAN',      1972, NULL::INTEGER, true, 2),
    ('7 Series', '7SERIES', '7-series', 'Full-size luxury sedan',             'SEDAN',      1977, NULL::INTEGER, true, 3),
    ('X3',       'X3',      'x3',       'Compact luxury SUV',                 'SUV',        2003, NULL::INTEGER, true, 4),
    ('X5',       'X5',      'x5',       'Mid-size luxury SUV',                'SUV',        1999, NULL::INTEGER, true, 5),
    ('X7',       'X7',      'x7',       'Full-size luxury SUV',               'SUV',        2018, NULL::INTEGER, true, 6),
    ('M3',       'M3',      'm3',       'High-performance sedan',             'SPORTS',     1986, NULL::INTEGER, true, 7),
    ('M4',       'M4',      'm4',       'High-performance coupe/convertible', 'SPORTS',     2014, NULL::INTEGER, true, 8),
    ('iX',       'IX',      'ix',       'All-electric luxury SUV',            'SUV',        2021, NULL::INTEGER, true, 9),
    ('S1000RR',  'S1000RR', 's1000rr',  'Superbike',                          'SPORT_BIKE', 2009, NULL::INTEGER, true, 10),
    ('R1250GS',  'R1250GS', 'r1250gs',  'Adventure touring motorcycle',       'ADVENTURE',  2018, NULL::INTEGER, true, 11)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'BMW';

-- Maruti Suzuki Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Swift',        'SWIFT',       'swift',        'Compact hatchback',      'HATCH',   2005, NULL::INTEGER, true, 1),
    ('Baleno',       'BALENO',      'baleno',       'Premium hatchback',      'HATCH',   2015, NULL::INTEGER, true, 2),
    ('Dzire',        'DZIRE',       'dzire',        'Compact sedan',          'SEDAN',   2008, NULL::INTEGER, true, 3),
    ('Ertiga',       'ERTIGA',      'ertiga',       'Compact MPV',            'MINIVAN', 2012, NULL::INTEGER, true, 4),
    ('Brezza',       'BREZZA',      'brezza',       'Compact SUV',            'SUV',     2016, NULL::INTEGER, true, 5),
    ('Grand Vitara', 'GRANDVITARA', 'grand-vitara', 'Mid-size hybrid SUV',    'SUV',     2022, NULL::INTEGER, true, 6),
    ('Jimny',        'JIMNY',       'jimny',        'Compact off-road SUV',   'SUV',     2023, NULL::INTEGER, true, 7),
    ('Wagon R',      'WAGONR',      'wagon-r',      'Tall hatchback',         'HATCH',   1999, NULL::INTEGER, true, 8),
    ('Alto',         'ALTO',        'alto',         'Entry-level hatchback',  'HATCH',   2000, NULL::INTEGER, true, 9),
    ('Ciaz',         'CIAZ',        'ciaz',         'Mid-size sedan',         'SEDAN',   2014, NULL::INTEGER, true, 10)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'MARUTI';

-- Tata Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Nexon',    'NEXON',    'nexon',    'Compact SUV / EV',         'SUV',    2017, NULL::INTEGER, true, 1),
    ('Harrier',  'HARRIER',  'harrier',  'Mid-size SUV',             'SUV',    2019, NULL::INTEGER, true, 2),
    ('Safari',   'SAFARI',   'safari',   'Full-size SUV',            'SUV',    2021, NULL::INTEGER, true, 3),
    ('Punch',    'PUNCH',    'punch',    'Micro SUV',                'SUV',    2021, NULL::INTEGER, true, 4),
    ('Altroz',   'ALTROZ',   'altroz',   'Premium hatchback',        'HATCH',  2020, NULL::INTEGER, true, 5),
    ('Tiago',    'TIAGO',    'tiago',    'Entry hatchback / EV',     'HATCH',  2016, NULL::INTEGER, true, 6),
    ('Tigor',    'TIGOR',    'tigor',    'Compact sedan / EV',       'SEDAN',  2017, NULL::INTEGER, true, 7),
    ('Curvv',    'CURVV',    'curvv',    'Coupe SUV / EV',           'SUV',    2024, NULL::INTEGER, true, 8),
    ('Ace',      'ACE',      'ace',      'Mini truck',               'TRUCK',  2005, NULL::INTEGER, true, 9),
    ('Prima',    'PRIMA',    'prima',    'Heavy commercial truck',   'TRUCK',  2009, NULL::INTEGER, true, 10)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'TATA';

-- Mahindra Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Thar',         'THAR',       'thar',        'Compact off-road SUV',   'SUV',    2010, NULL::INTEGER, true, 1),
    ('Scorpio-N',    'SCORPION',   'scorpio-n',   'Full-size SUV',          'SUV',    2022, NULL::INTEGER, true, 2),
    ('XUV700',       'XUV700',     'xuv700',      'Premium mid-size SUV',   'SUV',    2021, NULL::INTEGER, true, 3),
    ('XUV400',       'XUV400',     'xuv400',      'Electric compact SUV',   'SUV',    2023, NULL::INTEGER, true, 4),
    ('Bolero',       'BOLERO',     'bolero',      'Utility vehicle',        'SUV',    2000, NULL::INTEGER, true, 5),
    ('BE 6',         'BE6',        'be-6',        'Electric coupe SUV',     'SUV',    2024, NULL::INTEGER, true, 6),
    ('XEV 9e',       'XEV9E',      'xev-9e',      'Electric full-size SUV', 'SUV',    2024, NULL::INTEGER, true, 7),
    ('Bolero Pik-Up','BOLEROPKUP', 'bolero-pikup','Compact pickup truck',   'PICKUP', 2010, NULL::INTEGER, true, 8)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'MAHINDRA';

-- Royal Enfield Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Classic 350',  'CLASSIC350',  'classic-350',  'Classic cruiser motorcycle', 'CRUISER',    2009, NULL::INTEGER, true, 1),
    ('Bullet 350',   'BULLET350',   'bullet-350',   'Heritage commuter motorcycle','COMMUTER',  1955, NULL::INTEGER, true, 2),
    ('Meteor 350',   'METEOR350',   'meteor-350',   'Entry cruiser motorcycle',   'CRUISER',    2020, NULL::INTEGER, true, 3),
    ('Himalayan',    'HIMALAYAN',   'himalayan',    'Adventure tourer',           'ADVENTURE',  2016, NULL::INTEGER, true, 4),
    ('Hunter 350',   'HUNTER350',   'hunter-350',   'Urban roadster',             'COMMUTER',   2022, NULL::INTEGER, true, 5),
    ('Super Meteor', 'SUPERMETEOR', 'super-meteor', 'Mid-size cruiser',           'CRUISER',    2023, NULL::INTEGER, true, 6),
    ('Guerrilla 450','GUERRILLA450','guerrilla-450','Street motorcycle',          'SPORT_BIKE', 2024, NULL::INTEGER, true, 7),
    ('Bear 650',     'BEAR650',     'bear-650',     'Scrambler motorcycle',       'ADVENTURE',  2024, NULL::INTEGER, true, 8)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'ROYALENFIELD';

-- Trek Bicycle Models
INSERT INTO public.models (brand_id, name, code, slug, description, vehicle_type_id, start_year, end_year, is_current_model, display_order)
SELECT b.id, m.name, m.code, m.slug, m.description, vt.id, m.start_year::INTEGER, m.end_year::INTEGER, m.is_current_model, m.display_order
FROM public.brands b
CROSS JOIN (VALUES
    ('Domane',  'DOMANE',  'domane',  'Endurance road bike',           'ROAD_BIKE',   2012, NULL::INTEGER, true, 1),
    ('Madone',  'MADONE',  'madone',  'Aero road bike',                'ROAD_BIKE',   2003, NULL::INTEGER, true, 2),
    ('Fuel EX', 'FUEL_EX', 'fuel-ex', 'Full suspension mountain bike', 'MTB',         2005, NULL::INTEGER, true, 3),
    ('Marlin',  'MARLIN',  'marlin',  'Hardtail mountain bike',        'MTB',         2010, NULL::INTEGER, true, 4),
    ('FX',      'FX',      'fx',      'Fitness hybrid bike',           'HYBRID_BIKE', 2000, NULL::INTEGER, true, 5),
    ('Verve',   'VERVE',   'verve',   'Comfort hybrid bike',           'HYBRID_BIKE', 2010, NULL::INTEGER, true, 6),
    ('Rail',    'RAIL',    'rail',    'Electric mountain bike',        'EBIKE',       2019, NULL::INTEGER, true, 7),
    ('Allant',  'ALLANT',  'allant',  'Electric city bike',            'EBIKE',       2019, NULL::INTEGER, true, 8)
) AS m(name, code, slug, description, vehicle_type_code, start_year, end_year, is_current_model, display_order)
JOIN public.vehicle_types vt ON vt.code = m.vehicle_type_code
WHERE b.code = 'TREK';

-- Verify seed data
SELECT
    b.name   AS brand,
    m.name   AS model,
    vt.name  AS vehicle_type,
    m.start_year,
    m.end_year,
    m.is_current_model
FROM public.models m
JOIN public.brands b         ON m.brand_id = b.id
JOIN public.vehicle_types vt ON m.vehicle_type_id = vt.id
ORDER BY b.display_order, m.display_order;