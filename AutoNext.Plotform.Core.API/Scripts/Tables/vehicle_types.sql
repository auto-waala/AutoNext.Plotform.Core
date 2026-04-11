-- =====================================================
-- Script 2: Create Vehicle Types Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.vehicle_types CASCADE;

-- Create vehicle types table
CREATE TABLE public.vehicle_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    category_id UUID NOT NULL,
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    slug VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    icon_url VARCHAR(500),
    image_url VARCHAR(500),
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    
    CONSTRAINT fk_vehicle_types_category FOREIGN KEY (category_id) 
        REFERENCES public.categories(id) ON DELETE CASCADE
);

-- Create indexes
CREATE INDEX idx_vehicle_types_category ON public.vehicle_types(category_id);
CREATE INDEX idx_vehicle_types_slug ON public.vehicle_types(slug);
CREATE INDEX idx_vehicle_types_code ON public.vehicle_types(code);
CREATE INDEX idx_vehicle_types_is_active ON public.vehicle_types(is_active);

-- Add table comments
COMMENT ON TABLE public.vehicle_types IS 'Specific vehicle types under each category';
COMMENT ON COLUMN public.vehicle_types.category_id IS 'Reference to parent category';
COMMENT ON COLUMN public.vehicle_types.name IS 'Vehicle type display name';
COMMENT ON COLUMN public.vehicle_types.code IS 'Unique code for system reference';
COMMENT ON COLUMN public.vehicle_types.slug IS 'URL-friendly identifier';
COMMENT ON COLUMN public.vehicle_types.metadata IS 'JSON schema for dynamic attributes';

-- Create audit trigger
CREATE TRIGGER update_vehicle_types_updated_at 
    BEFORE UPDATE ON public.vehicle_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Vehicle types table created successfully' as Status;