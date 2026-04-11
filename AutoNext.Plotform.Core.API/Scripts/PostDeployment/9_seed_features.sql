-- =====================================================
-- Script 9: Seed Features Data
-- =====================================================

INSERT INTO public.features (name, code, category, sub_category, icon_url, applicable_categories, is_standard, display_order, is_active) VALUES
    -- Safety Features
    ('Airbags', 'AIRBAGS', 'Safety', 'Passive Safety', '/icons/features/airbags.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 1, true),
    ('ABS', 'ABS', 'Safety', 'Braking System', '/icons/features/abs.svg', '["CARS", "BIKES", "SUV", "TRUCKS", "VANS"]', true, 2, true),
    ('ESC', 'ESC', 'Safety', 'Stability Control', '/icons/features/esc.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 3, true),
    ('Traction Control', 'TCS', 'Safety', 'Stability Control', '/icons/features/tcs.svg', '["CARS", "BIKES", "SUV", "TRUCKS"]', true, 4, true),
    ('Hill Start Assist', 'HSA', 'Safety', 'Driver Assist', '/icons/features/hill-assist.svg', '["CARS", "SUV", "TRUCKS"]', false, 5, true),
    ('Lane Departure Warning', 'LDW', 'Safety', 'Driver Assist', '/icons/features/lane-departure.svg', '["CARS", "SUV"]', false, 6, true),
    ('Blind Spot Monitoring', 'BSM', 'Safety', 'Driver Assist', '/icons/features/blind-spot.svg', '["CARS", "SUV"]', false, 7, true),
    ('Adaptive Cruise Control', 'ACC', 'Safety', 'Driver Assist', '/icons/features/adaptive-cruise.svg', '["CARS", "SUV"]', false, 8, true),
    ('Rear View Camera', 'REAR_CAM', 'Safety', 'Parking Assist', '/icons/features/rear-camera.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 9, true),
    ('Parking Sensors', 'PARK_SENSOR', 'Safety', 'Parking Assist', '/icons/features/parking-sensor.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', false, 10, true),
    ('360 Camera', '360_CAM', 'Safety', 'Parking Assist', '/icons/features/360-camera.svg', '["CARS", "SUV", "TRUCKS"]', false, 11, true),
    ('Tire Pressure Monitoring', 'TPMS', 'Safety', 'Monitoring', '/icons/features/tpms.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 12, true),
    
    -- Comfort Features
    ('Air Conditioning', 'AC', 'Comfort', 'Climate Control', '/icons/features/ac.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 13, true),
    ('Climate Control', 'CLIMATE_CTRL', 'Comfort', 'Climate Control', '/icons/features/climate-control.svg', '["CARS", "SUV", "TRUCKS"]', false, 14, true),
    ('Heated Seats', 'HEATED_SEATS', 'Comfort', 'Seating', '/icons/features/heated-seats.svg', '["CARS", "SUV"]', false, 15, true),
    ('Ventilated Seats', 'VENT_SEATS', 'Comfort', 'Seating', '/icons/features/ventilated-seats.svg', '["CARS", "SUV", "LUXURY"]', false, 16, true),
    ('Power Seats', 'POWER_SEATS', 'Comfort', 'Seating', '/icons/features/power-seats.svg', '["CARS", "SUV"]', false, 17, true),
    ('Leather Seats', 'LEATHER', 'Comfort', 'Seating', '/icons/features/leather.svg', '["CARS", "SUV", "LUXURY"]', false, 18, true),
    ('Sunroof', 'SUNROOF', 'Comfort', 'Roof', '/icons/features/sunroof.svg', '["CARS", "SUV"]', false, 19, true),
    ('Panoramic Roof', 'PANOROOF', 'Comfort', 'Roof', '/icons/features/panoramic-roof.svg', '["CARS", "SUV", "LUXURY"]', false, 20, true),
    ('Keyless Entry', 'KEYLESS', 'Comfort', 'Convenience', '/icons/features/keyless.svg', '["CARS", "SUV", "TRUCKS"]', true, 21, true),
    ('Push Start', 'PUSH_START', 'Comfort', 'Convenience', '/icons/features/push-start.svg', '["CARS", "SUV", "TRUCKS"]', true, 22, true),
    ('Cruise Control', 'CRUISE', 'Comfort', 'Convenience', '/icons/features/cruise-control.svg', '["CARS", "SUV", "TRUCKS"]', true, 23, true),
    ('Power Windows', 'PWR_WINDOWS', 'Comfort', 'Convenience', '/icons/features/power-windows.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 24, true),
    ('Power Locks', 'PWR_LOCKS', 'Comfort', 'Convenience', '/icons/features/power-locks.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 25, true),
    
    -- Entertainment Features
    ('Bluetooth', 'BLUETOOTH', 'Entertainment', 'Audio', '/icons/features/bluetooth.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 26, true),
    ('Touch Screen', 'TOUCH_SCREEN', 'Entertainment', 'Display', '/icons/features/touch-screen.svg', '["CARS", "SUV", "TRUCKS"]', true, 27, true),
    ('Apple CarPlay', 'CARPLAY', 'Entertainment', 'Connectivity', '/icons/features/carplay.svg', '["CARS", "SUV", "TRUCKS"]', false, 28, true),
    ('Android Auto', 'ANDROID_AUTO', 'Entertainment', 'Connectivity', '/icons/features/android-auto.svg', '["CARS", "SUV", "TRUCKS"]', false, 29, true),
    ('GPS Navigation', 'NAV', 'Entertainment', 'Navigation', '/icons/features/navigation.svg', '["CARS", "SUV", "TRUCKS"]', false, 30, true),
    ('Premium Sound System', 'PREMIUM_SOUND', 'Entertainment', 'Audio', '/icons/features/premium-sound.svg', '["CARS", "SUV", "LUXURY"]', false, 31, true),
    ('Rear Entertainment', 'REAR_ENT', 'Entertainment', 'Video', '/icons/features/rear-entertainment.svg', '["CARS", "SUV", "VANS"]', false, 32, true),
    ('USB Ports', 'USB', 'Entertainment', 'Connectivity', '/icons/features/usb.svg', '["CARS", "SUV", "TRUCKS", "VANS"]', true, 33, true),
    ('Wireless Charging', 'WIRELESS_CHARGE', 'Entertainment', 'Connectivity', '/icons/features/wireless-charge.svg', '["CARS", "SUV", "LUXURY"]', false, 34, true),
    
    -- Performance Features
    ('Turbo Charged', 'TURBO', 'Performance', 'Engine', '/icons/features/turbo.svg', '["CARS", "SUV", "SPORTS"]', false, 35, true),
    ('All Wheel Drive', 'AWD', 'Performance', 'Drivetrain', '/icons/features/awd.svg', '["CARS", "SUV"]', false, 36, true),
    ('4 Wheel Drive', '4WD', 'Performance', 'Drivetrain', '/icons/features/4wd.svg', '["SUV", "TRUCKS"]', false, 37, true),
    ('Limited Slip Differential', 'LSD', 'Performance', 'Drivetrain', '/icons/features/lsd.svg', '["SPORTS", "SUV"]', false, 38, true),
    ('Sport Mode', 'SPORT_MODE', 'Performance', 'Drive Modes', '/icons/features/sport-mode.svg', '["CARS", "SUV", "SPORTS"]', false, 39, true),
    ('Paddle Shifters', 'PADDLE_SHIFT', 'Performance', 'Transmission', '/icons/features/paddle-shift.svg', '["CARS", "SPORTS"]', false, 40, true),
    
    -- Motorcycle Specific Features
    ('ABS (Motorcycle)', 'MOTO_ABS', 'Safety', 'Braking', '/icons/features/moto-abs.svg', '["BIKES"]', false, 41, true),
    ('Traction Control (Moto)', 'MOTO_TCS', 'Safety', 'Stability', '/icons/features/moto-tcs.svg', '["BIKES"]', false, 42, true),
    ('Quick Shifter', 'QUICK_SHIFT', 'Performance', 'Transmission', '/icons/features/quick-shift.svg', '["BIKES"]', false, 43, true),
    ('Riding Modes', 'RIDE_MODES', 'Performance', 'Electronics', '/icons/features/ride-modes.svg', '["BIKES"]', false, 44, true),
    ('Heated Grips', 'HEATED_GRIPS', 'Comfort', 'Handlebars', '/icons/features/heated-grips.svg', '["BIKES"]', false, 45, true),
    ('Saddle Bags', 'SADDLE_BAGS', 'Utility', 'Storage', '/icons/features/saddle-bags.svg', '["BIKES"]', false, 46, true),
    ('Windshield', 'WINDSCREEN', 'Comfort', 'Aerodynamics', '/icons/features/windscreen.svg', '["BIKES"]', false, 47, true),
    
    -- Bicycle Specific Features
    ('Disc Brakes', 'DISC_BRAKES', 'Safety', 'Braking', '/icons/features/disc-brakes.svg', '["CYCLES"]', false, 48, true),
    ('Carbon Frame', 'CARBON_FRAME', 'Performance', 'Frame', '/icons/features/carbon-frame.svg', '["CYCLES"]', false, 49, true),
    ('Suspension Fork', 'SUSPENSION_FORK', 'Comfort', 'Suspension', '/icons/features/suspension.svg', '["CYCLES"]', false, 50, true),
    ('LED Lights', 'LED_LIGHTS', 'Safety', 'Lighting', '/icons/features/led-lights.svg', '["CYCLES"]', false, 51, true),
    
    -- Truck Specific Features
    ('Tow Package', 'TOW_PKG', 'Utility', 'Towing', '/icons/features/tow-package.svg', '["TRUCKS"]', false, 52, true),
    ('Bed Liner', 'BED_LINER', 'Utility', 'Cargo', '/icons/features/bed-liner.svg', '["TRUCKS"]', false, 53, true),
    ('Cargo Management', 'CARGO_MGMT', 'Utility', 'Storage', '/icons/features/cargo-mgmt.svg', '["TRUCKS"]', false, 54, true),
    ('Running Boards', 'RUNNING_BOARDS', 'Comfort', 'Access', '/icons/features/running-boards.svg', '["TRUCKS", "SUV"]', false, 55, true);

-- Verify seed data
SELECT category, COUNT(*) as feature_count FROM public.features GROUP BY category ORDER BY category;