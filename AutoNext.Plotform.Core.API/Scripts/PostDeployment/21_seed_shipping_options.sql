-- =====================================================
-- Script 21: Seed Shipping Options Data
-- =====================================================

INSERT INTO public.shipping_options (name, code, description, provider, estimated_days_min, estimated_days_max, base_cost, cost_per_km, is_tracking_available, is_insurance_available, applicable_vehicle_types, display_order) VALUES
    ('Standard Auto Transport', 'STANDARD_AUTO', 'Open carrier auto transport', 'Standard Transport', 5, 10, 200.00, 0.50, true, true, '["CARS", "SUV", "TRUCKS"]', 1),
    ('Enclosed Auto Transport', 'ENCLOSED_AUTO', 'Enclosed carrier for luxury/exotic cars', 'Premium Transport', 5, 12, 500.00, 0.80, true, true, '["CARS", "SUV", "LUXURY"]', 2),
    ('Motorcycle Shipping', 'MOTO_SHIP', 'Specialized motorcycle transport', 'Moto Haul', 4, 8, 150.00, 0.40, true, true, '["BIKES", "SCOOTERS"]', 3),
    ('Bicycle Shipping', 'BIKE_SHIP', 'Bicycle box shipping via courier', 'Bike Courier', 3, 7, 50.00, 0.00, true, false, '["CYCLES"]', 4),
    ('Local Delivery', 'LOCAL', 'Local delivery within 50km', 'Local Delivery', 1, 2, 50.00, 1.00, true, true, '["CARS", "BIKES", "CYCLES", "TRUCKS"]', 5),
    ('Door-to-Door', 'DOOR_TO_DOOR', 'Full door-to-door service', 'Premium Transport', 3, 7, 300.00, 0.60, true, true, '["CARS", "SUV", "LUXURY"]', 6),
    ('Terminal to Terminal', 'TERMINAL', 'Ship between terminals (cheaper)', 'Economy Transport', 7, 14, 150.00, 0.30, false, false, '["CARS", "TRUCKS"]', 7),
    ('Express Shipping', 'EXPRESS', 'Express delivery for small vehicles', 'Express Transport', 2, 4, 250.00, 0.70, true, true, '["BIKES", "CYCLES", "SCOOTERS"]', 8),
    ('International Shipping', 'INTERNATIONAL', 'International vehicle shipping', 'Global Transport', 20, 45, 1000.00, 2.00, true, true, '["CARS", "BIKES", "TRUCKS"]', 9);

SELECT 'Shipping options seeded successfully' as Status;