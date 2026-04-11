-- =====================================================
-- Script 18: Create Title Types Table
-- =====================================================

-- Drop table if exists
DROP TABLE IF EXISTS public.title_types CASCADE;

-- Create title types table
CREATE TABLE public.title_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    description TEXT,
    is_clean BOOLEAN DEFAULT true,
    affects_value BOOLEAN DEFAULT false,
    value_deduction_percentage DECIMAL(5,2),
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);

-- Create indexes
CREATE INDEX idx_title_types_code ON public.title_types(code);
CREATE INDEX idx_title_types_is_clean ON public.title_types(is_clean);

-- Create audit trigger
CREATE TRIGGER update_title_types_updated_at 
    BEFORE UPDATE ON public.title_types 
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Verify table creation
SELECT 'Title types table created successfully' as Status;