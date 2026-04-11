-- =====================================================
-- Script 1: Seed Categories Data
-- =====================================================

-- Insert main categories
INSERT INTO public.categories (name, code, slug, description, icon_url, display_order, is_active, metadata) VALUES
    ('Cars', 'CARS', 'cars', 'Passenger vehicles including sedans, SUVs, hatchbacks, and luxury cars', '/icons/categories/cars.svg', 1, true, '{"features": ["airbags", "ac", "power_windows"], "has_fuel_type": true, "has_transmission": true}'),
    ('Motorcycles', 'BIKES', 'motorcycles', 'Two-wheelers including sport bikes, cruisers, scooters, and touring motorcycles', '/icons/categories/bikes.svg', 2, true, '{"features": ["helmet", "abs"], "has_fuel_type": true, "has_transmission": true}'),
    ('Trucks', 'TRUCKS', 'trucks', 'Commercial and personal trucks including pickups, box trucks, and heavy duty vehicles', '/icons/categories/trucks.svg', 3, true, '{"features": ["tow_hitch", "bed_liner"], "has_fuel_type": true, "has_transmission": true}'),
    ('Bicycles', 'CYCLES', 'bicycles', 'Human-powered cycles including road bikes, mountain bikes, hybrid, and electric bicycles', '/icons/categories/cycles.svg', 4, true, '{"features": ["helmet_required"], "has_fuel_type": false, "has_transmission": true}'),
    ('SUVs & Crossovers', 'SUV', 'suvs-crossovers', 'Sport Utility Vehicles and Crossovers for family and adventure', '/icons/categories/suv.svg', 5, true, '{"features": ["4x4", "roof_rails"], "has_fuel_type": true, "has_transmission": true}'),
    ('Vans & Minivans', 'VANS', 'vans-minivans', 'Passenger and cargo vans for family and commercial use', '/icons/categories/vans.svg', 6, true, '{"features": ["sliding_doors", "cargo_space"], "has_fuel_type": true, "has_transmission": true}'),
    ('Electric Vehicles', 'EV', 'electric-vehicles', 'Electric cars, bikes, and scooters', '/icons/categories/ev.svg', 7, true, '{"features": ["charging_port", "battery"], "has_fuel_type": false, "has_transmission": true}'),
    ('Heavy Equipment', 'HEAVY', 'heavy-equipment', 'Construction and agricultural machinery', '/icons/categories/heavy.svg', 8, true, '{"features": ["hydraulics", "attachments"], "has_fuel_type": true, "has_transmission": true}'),
    ('Trailers', 'TRAILERS', 'trailers', 'Cargo, boat, and utility trailers', '/icons/categories/trailers.svg', 9, true, '{"features": ["hitch_type"], "has_fuel_type": false, "has_transmission": false}'),
    ('Scooters & Mopeds', 'SCOOTERS', 'scooters-mopeds', 'Small engine scooters and mopeds for city commuting', '/icons/categories/scooters.svg', 10, true, '{"features": ["storage_box"], "has_fuel_type": true, "has_transmission": true}');

-- Verify seed data
SELECT id, name, code, slug, display_order FROM public.categories ORDER BY display_order;