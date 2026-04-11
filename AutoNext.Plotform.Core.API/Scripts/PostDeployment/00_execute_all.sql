-- =====================================================
-- Master Script: Execute all table creations in order
-- =====================================================

-- Run scripts in sequence
\i 1_create_categories_table.sql
\i 1_seed_categories.sql

\i 2_create_vehicle_types_table.sql
\i 2_seed_vehicle_types.sql

\i 3_create_fuel_types_table.sql
\i 3_seed_fuel_types.sql

\i 4_create_transmissions_table.sql
\i 4_seed_transmissions.sql

\i 5_create_brands_table.sql
\i 5_seed_brands.sql

\i 6_create_models_table.sql
\i 6_seed_models.sql

\i 7_create_conditions_table.sql
\i 7_seed_conditions.sql

\i 8_create_colors_table.sql
\i 8_seed_colors.sql

-- Final verification
SELECT 'All tables created and seeded successfully!' as Status;
SELECT 
    'Categories' as Table_Name, COUNT(*) as Record_Count FROM categories
UNION ALL
SELECT 'Vehicle Types', COUNT(*) FROM vehicle_types
UNION ALL
SELECT 'Fuel Types', COUNT(*) FROM fuel_types
UNION ALL
SELECT 'Transmissions', COUNT(*) FROM transmissions
UNION ALL
SELECT 'Brands', COUNT(*) FROM brands
UNION ALL
SELECT 'Models', COUNT(*) FROM models
UNION ALL
SELECT 'Conditions', COUNT(*) FROM vehicle_conditions
UNION ALL
SELECT 'Colors', COUNT(*) FROM colors;