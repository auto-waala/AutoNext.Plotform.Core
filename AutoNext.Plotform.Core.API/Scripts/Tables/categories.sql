-- =====================================================
-- Script 1: Create Categories Table
-- =====================================================

-- Enable UUID extension if not already enabled
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Drop table if exists
DROP TABLE IF EXISTS public.categories CASCADE;

-- Create categories table
CREATE TABLE public.categories (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(100) NOT NULL,
    code VARCHAR(50) NOT NULL UNIQUE,
    slug VARCHAR(100) NOT NULL UNIQUE,
    description TEXT,
    icon_url VARCHAR(500),
    image_url VARCHAR(500),
    parent_category_id UUID NULL,
    display_order INTEGER NOT NULL DEFAULT 0,
    is_active BOOLEAN NOT NULL DEFAULT true,
    metadata JSONB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP,
    
    CONSTRAINT fk_categories_parent FOREIGN KEY (parent_category_id) 
        REFERENCES public.categories(id) ON DELETE SET NULL
);
-- Add table comments
COMMENT ON TABLE public.categories IS 'Main vehicle categories (Cars, Bikes, Trucks, Cycles, etc.)';
COMMENT ON COLUMN public.categories.id IS 'Unique identifier';
COMMENT ON COLUMN public.categories.name IS 'Category display name';
COMMENT ON COLUMN public.categories.code IS 'Unique code for system reference';
COMMENT ON COLUMN public.categories.slug IS 'URL-friendly identifier';
COMMENT ON COLUMN public.categories.description IS 'Category description';
COMMENT ON COLUMN public.categories.icon_url IS 'Icon URL for the category';
COMMENT ON COLUMN public.categories.image_url IS 'Banner image URL';
COMMENT ON COLUMN public.categories.parent_category_id IS 'Parent category for subcategories';
COMMENT ON COLUMN public.categories.display_order IS 'Order for display sorting';
COMMENT ON COLUMN public.categories.is_active IS 'Enable/disable category';
COMMENT ON COLUMN public.categories.metadata IS 'JSON field for additional category data';
COMMENT ON COLUMN public.categories.created_at IS 'Creation timestamp';
COMMENT ON COLUMN public.categories.updated_at IS 'Last update timestamp';
