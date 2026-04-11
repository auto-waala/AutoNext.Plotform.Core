-- =====================================================
-- Script 17: Create Warranty Types Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.warranty_types CASCADE;

-- Create warranty types table
CREATE TABLE public.warranty_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    duration_months INTEGER,
    duration_km INTEGER,
    is_transferable BOOLEAN DEFAULT false,
    applicable_categories JSONB,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_warranty_types_code ON public.warranty_types(code);
CREATE INDEX idx_warranty_types_is_active ON public.warranty_types(is_active);

-- Add table comments
COMMENT ON TABLE public.warranty_types IS 'Types of warranties offered with vehicles';

-- Create audit trigger
CREATE TRIGGER update_warranty_types_updated_at 
    BEFORE UPDATE ON public.warranty_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Warranty types table created successfully' as Status;