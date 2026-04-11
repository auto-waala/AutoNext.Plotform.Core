-- =====================================================
-- Script 9: Create Features Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.features CASCADE;

-- Create features table
CREATE TABLE public.features (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    category VARCHAR(50), -- Safety, Comfort, Entertainment, Performance, etc.
    sub_category VARCHAR(50),
    icon_url VARCHAR(500),
    applicable_categories JSONB, -- Which vehicle categories this feature applies to
    is_standard BOOLEAN DEFAULT false, -- Is this feature standard or optional
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_features_code ON public.features(code);
CREATE INDEX idx_features_category ON public.features(category);
CREATE INDEX idx_features_is_active ON public.features(is_active);

-- Add table comments
COMMENT ON TABLE public.features IS 'Vehicle features and amenities';
COMMENT ON COLUMN public.features.applicable_categories IS 'JSON array of category codes this feature applies to';

-- Create audit trigger
CREATE TRIGGER update_features_updated_at 
    BEFORE UPDATE ON public.features 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Features table created successfully' as Status;