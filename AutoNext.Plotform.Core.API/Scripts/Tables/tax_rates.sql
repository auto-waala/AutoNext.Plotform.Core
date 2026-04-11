-- =====================================================
-- Script 22: Create Tax Rates Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.tax_rates CASCADE;

-- Create tax rates table
CREATE TABLE public.tax_rates (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    tax_type VARCHAR(50), -- GST, VAT, Sales Tax, etc.
    country VARCHAR(100),
    state VARCHAR(100),
    city VARCHAR(100),
    rate_percentage DECIMAL(5,2) NOT NULL,
    is_compound BOOLEAN DEFAULT false,
    applies_to_vehicle_types JSONB,
    min_price_threshold DECIMAL(12,2),
    max_price_threshold DECIMAL(12,2),
    effective_from DATE NOT NULL,
    effective_to DATE,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_tax_rates_code ON public.tax_rates(code);
CREATE INDEX idx_tax_rates_location ON public.tax_rates(country, state);
CREATE INDEX idx_tax_rates_effective ON public.tax_rates(effective_from, effective_to);

-- Create audit trigger
CREATE TRIGGER update_tax_rates_updated_at 
    BEFORE UPDATE ON public.tax_rates 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Tax rates table created successfully' as Status;