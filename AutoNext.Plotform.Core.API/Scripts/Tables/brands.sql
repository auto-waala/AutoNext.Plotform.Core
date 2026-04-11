-- =====================================================
-- Script 5: Create Brands Table
-- =====================================================

-- Ensure the trigger function exists (safe to re-run)
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Drop table if exists
DROP TABLE IF EXISTS public.brands CASCADE;

-- Create brands table
CREATE TABLE public.brands (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    slug VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    logo_url VARCHAR(500),
    website_url VARCHAR(255),
    country_of_origin VARCHAR(100),
    founded_year INTEGER,
    applicable_categories JSONB, -- Which categories this brand produces
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_brands_name ON public.brands(name);
CREATE INDEX idx_brands_code ON public.brands(code);
CREATE INDEX idx_brands_slug ON public.brands(slug);
CREATE INDEX idx_brands_is_active ON public.brands(is_active);

-- Add table comments
COMMENT ON TABLE public.brands IS 'Vehicle manufacturers/brands';
COMMENT ON COLUMN public.brands.applicable_categories IS 'JSON array of category codes this brand manufactures';

-- Create audit trigger
CREATE TRIGGER update_brands_updated_at 
    BEFORE UPDATE ON public.brands 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Brands table created successfully' as Status;