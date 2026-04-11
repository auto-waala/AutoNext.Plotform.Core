-- =====================================================
-- Script 20: Seed Payment Methods Data
-- =====================================================

INSERT INTO public.payment_methods (name, code, type, icon_url, processing_fee_percentage, processing_fee_fixed, settlement_days, is_instant, display_order) VALUES
    ('Cash', 'CASH', 'Cash', '/icons/payments/cash.svg', 0, 0, 0, true, 1),
    ('Bank Transfer', 'BANK_TRANSFER', 'Bank Transfer', '/icons/payments/bank.svg', 0, 0, 2, false, 2),
    ('Credit Card', 'CREDIT_CARD', 'Card', '/icons/payments/credit-card.svg', 2.9, 0.30, 3, false, 3),
    ('Debit Card', 'DEBIT_CARD', 'Card', '/icons/payments/debit-card.svg', 2.5, 0.25, 2, false, 4),
    ('PayPal', 'PAYPAL', 'Digital Wallet', '/icons/payments/paypal.svg', 3.5, 0.30, 1, true, 5),
    ('Stripe', 'STRIPE', 'Digital Wallet', '/icons/payments/stripe.svg', 2.9, 0.30, 2, true, 6),
    ('Cryptocurrency', 'CRYPTO', 'Digital Wallet', '/icons/payments/crypto.svg', 1.0, 0, 0, true, 7),
    ('Cashier Check', 'CASHIER_CHECK', 'Check', '/icons/payments/check.svg', 0, 0, 5, false, 8),
    ('Financing', 'FINANCING', 'Financing', '/icons/payments/financing.svg', 0, 0, 7, false, 9),
    ('Buy Now Pay Later', 'BNPL', 'Financing', '/icons/payments/bnpl.svg', 3.0, 0, 0, true, 10),
    ('Google Pay', 'GOOGLE_PAY', 'Digital Wallet', '/icons/payments/google-pay.svg', 2.9, 0.30, 1, true, 11),
    ('Apple Pay', 'APPLE_PAY', 'Digital Wallet', '/icons/payments/apple-pay.svg', 2.9, 0.30, 1, true, 12);

SELECT 'Payment methods seeded successfully' as Status;