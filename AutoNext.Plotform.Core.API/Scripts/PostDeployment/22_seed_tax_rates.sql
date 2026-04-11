-- =====================================================
-- Script 22: Seed Tax Rates Data
-- =====================================================

INSERT INTO public.tax_rates (name, code, tax_type, country, state, rate_percentage, applies_to_vehicle_types, effective_from, display_order) VALUES
    ('US Federal Tax', 'US_FED', 'Federal Tax', 'USA', NULL, 0, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 1),
    ('California Sales Tax', 'CA_SALES', 'Sales Tax', 'USA', 'California', 7.25, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 2),
    ('Texas Sales Tax', 'TX_SALES', 'Sales Tax', 'USA', 'Texas', 6.25, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 3),
    ('New York Sales Tax', 'NY_SALES', 'Sales Tax', 'USA', 'New York', 8.875, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 4),
    ('Florida Sales Tax', 'FL_SALES', 'Sales Tax', 'USA', 'Florida', 6.00, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 5),
    ('GST India', 'IND_GST', 'GST', 'India', NULL, 18, '["CARS", "BIKES", "TRUCKS"]', '2024-01-01', 6),
    ('GST India Bicycles', 'IND_GST_CYCLE', 'GST', 'India', NULL, 12, '["CYCLES"]', '2024-01-01', 7),
    ('UK VAT', 'UK_VAT', 'VAT', 'UK', NULL, 20, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 8),
    ('Canada GST', 'CAN_GST', 'GST', 'Canada', NULL, 5, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 9),
    ('Australia GST', 'AUS_GST', 'GST', 'Australia', NULL, 10, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', '2024-01-01', 10);

SELECT 'Tax rates seeded successfully' as Status;