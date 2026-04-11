-- =====================================================
-- Script 8: Seed Colors Data
-- =====================================================

-- Insert common vehicle colors
INSERT INTO public.colors (name, code, hex_code, rgb_value, display_order, is_active) VALUES
    ('Black', 'BLACK', '#000000', 'RGB(0,0,0)', 1, true),
    ('White', 'WHITE', '#FFFFFF', 'RGB(255,255,255)', 2, true),
    ('Silver', 'SILVER', '#C0C0C0', 'RGB(192,192,192)', 3, true),
    ('Gray', 'GRAY', '#808080', 'RGB(128,128,128)', 4, true),
    ('Red', 'RED', '#FF0000', 'RGB(255,0,0)', 5, true),
    ('Blue', 'BLUE', '#0000FF', 'RGB(0,0,255)', 6, true),
    ('Green', 'GREEN', '#008000', 'RGB(0,128,0)', 7, true),
    ('Yellow', 'YELLOW', '#FFFF00', 'RGB(255,255,0)', 8, true),
    ('Orange', 'ORANGE', '#FFA500', 'RGB(255,165,0)', 9, true),
    ('Brown', 'BROWN', '#8B4513', 'RGB(139,69,19)', 10, true),
    ('Beige', 'BEIGE', '#F5F5DC', 'RGB(245,245,220)', 11, true),
    ('Burgundy', 'BURGUNDY', '#800020', 'RGB(128,0,32)', 12, true),
    ('Navy Blue', 'NAVY', '#000080', 'RGB(0,0,128)', 13, true),
    ('Dark Blue', 'DARK_BLUE', '#00008B', 'RGB(0,0,139)', 14, true),
    ('Midnight Blue', 'MIDNIGHT_BLUE', '#191970', 'RGB(25,25,112)', 15, true),
    ('Charcoal', 'CHARCOAL', '#36454F', 'RGB(54,69,79)', 16, true),
    ('Titanium', 'TITANIUM', '#878681', 'RGB(135,134,129)', 17, true),
    ('Gold', 'GOLD', '#FFD700', 'RGB(255,215,0)', 18, true),
    ('Bronze', 'BRONZE', '#CD7F32', 'RGB(205,127,50)', 19, true),
    ('Purple', 'PURPLE', '#800080', 'RGB(128,0,128)', 20, true),
    ('Pink', 'PINK', '#FFC0CB', 'RGB(255,192,203)', 21, true),
    ('Matte Black', 'MATTE_BLACK', '#2C2C2C', 'RGB(44,44,44)', 22, true),
    ('Matte Gray', 'MATTE_GRAY', '#5E5E5E', 'RGB(94,94,94)', 23, true),
    ('Cyan', 'CYAN', '#00FFFF', 'RGB(0,255,255)', 24, true),
    ('Teal', 'TEAL', '#008080', 'RGB(0,128,128)', 25, true);

-- Verify seed data
SELECT name, code, hex_code FROM public.colors ORDER BY display_order;