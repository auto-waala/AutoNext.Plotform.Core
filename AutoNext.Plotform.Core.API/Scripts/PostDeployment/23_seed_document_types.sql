-- =====================================================
-- Script 23: Seed Document Types Data
-- =====================================================

INSERT INTO public.document_types (name, code, category, is_required, is_verifiable, expiry_months, applicable_vehicle_types, display_order) VALUES
    -- Vehicle Documents
    ('Vehicle Title', 'TITLE', 'Vehicle', true, true, NULL, '["CARS", "BIKES", "TRUCKS"]', 1),
    ('Registration Certificate', 'RC', 'Vehicle', true, true, 12, '["CARS", "BIKES", "TRUCKS"]', 2),
    ('Insurance Certificate', 'INSURANCE', 'Vehicle', true, true, 12, '["CARS", "BIKES", "TRUCKS"]', 3),
    ('Service History', 'SERVICE_HISTORY', 'Vehicle', false, false, NULL, '["CARS", "BIKES", "TRUCKS"]', 4),
    ('PUC Certificate', 'PUC', 'Vehicle', true, true, 6, '["CARS", "BIKES", "TRUCKS"]', 5),
    ('Warranty Document', 'WARRANTY', 'Vehicle', false, true, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 6),
    ('Bill of Sale', 'BILL_OF_SALE', 'Vehicle', true, false, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 7),
    
    -- Seller Documents
    ('Seller ID Proof', 'SELLER_ID', 'Seller', true, true, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 8),
    ('Seller Address Proof', 'SELLER_ADDRESS', 'Seller', true, true, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 9),
    ('Dealer License', 'DEALER_LICENSE', 'Seller', true, true, 12, '["CARS", "BIKES", "TRUCKS"]', 10),
    ('Business Registration', 'BUSINESS_REG', 'Seller', false, true, NULL, '["CARS", "BIKES", "TRUCKS"]', 11),
    
    -- Buyer Documents
    ('Buyer ID Proof', 'BUYER_ID', 'Buyer', true, true, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 12),
    ('Buyer Address Proof', 'BUYER_ADDRESS', 'Buyer', true, true, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 13),
    ('Driving License', 'DRIVING_LICENSE', 'Buyer', true, true, 60, '["CARS", "BIKES", "TRUCKS"]', 14),
    
    -- Transaction Documents
    ('Invoice', 'INVOICE', 'Transaction', true, false, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 15),
    ('Payment Receipt', 'PAYMENT_RECEIPT', 'Transaction', true, false, NULL, '["CARS", "BIKES", "TRUCKS", "CYCLES"]', 16),
    ('Transfer Form', 'TRANSFER_FORM', 'Transaction', true, false, NULL, '["CARS", "BIKES", "TRUCKS"]', 17),
    
    -- Bicycle Specific
    ('Bicycle Serial Number', 'BIKE_SERIAL', 'Vehicle', true, false, NULL, '["CYCLES"]', 18),
    ('Original Purchase Receipt', 'PURCHASE_RECEIPT', 'Vehicle', false, false, NULL, '["CYCLES"]', 19);

SELECT 'Document types seeded successfully' as Status;