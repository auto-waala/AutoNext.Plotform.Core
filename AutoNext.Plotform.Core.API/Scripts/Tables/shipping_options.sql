-- =====================================================
-- Script 21: Create Shipping Options Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.shipping_options CASCADE;

-- Create shipping options table
CREATE TABLE public.shipping_options (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    provider VARCHAR(100),
    estimated_days_min INTEGER,
    estimated_days_max INTEGER,
    base_cost DECIMAL(10,2),
    cost_per_km DECIMAL(6,2),
    is_tracking_available BOOLEAN DEFAULT false,
    is_insurance_available BOOLEAN DEFAULT false,
    applicable_vehicle_types JSONB,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_shipping_options_code ON public.shipping_options(code);
CREATE INDEX idx_shipping_options_provider ON public.shipping_options(provider);

-- Create audit trigger
CREATE TRIGGER update_shipping_options_updated_at 
    BEFORE UPDATE ON public.shipping_options 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Shipping options table created successfully' as Status;