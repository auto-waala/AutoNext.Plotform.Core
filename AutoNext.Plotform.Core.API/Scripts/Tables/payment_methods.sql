-- =====================================================
-- Script 20: Create Payment Methods Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.payment_methods CASCADE;

-- Create payment methods table
CREATE TABLE public.payment_methods (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    type VARCHAR(50), -- Cash, Card, Bank Transfer, Digital Wallet, Financing
    icon_url VARCHAR(500),
    processing_fee_percentage DECIMAL(5,2) DEFAULT 0,
    processing_fee_fixed DECIMAL(10,2) DEFAULT 0,
    settlement_days INTEGER,
    is_instant BOOLEAN DEFAULT false,
    is_available_for_sellers BOOLEAN DEFAULT true,
    is_available_for_buyers BOOLEAN DEFAULT true,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_payment_methods_code ON public.payment_methods(code);
CREATE INDEX idx_payment_methods_type ON public.payment_methods(type);

-- Create audit trigger
CREATE TRIGGER update_payment_methods_updated_at 
    BEFORE UPDATE ON public.payment_methods 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Payment methods table created successfully' as Status;