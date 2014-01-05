Select * From [Shopping].[ItemPicture]

SET IDENTITY_INSERT [Shopping].[Category] ON
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (1, 'TABLE', 'Tables');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (2, 'CHAIR', 'Chairs');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (3, 'COUCH', 'Couches');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (4, 'FRDGE', 'Fridges');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (5, 'COMP', 'Computers');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (6, 'APPLI', 'Appliances');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (7, 'TV', 'Televisions');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (8, 'RADIO', 'Radios');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (9, 'TENT', 'Tents');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (10, 'HIKE', 'Hiking');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (11, 'CYCLE', 'Cycling');
INSERT INTO [Shopping].[Category] ([CategoryID],[Code],[CategoryName]) VALUES (12, 'RUN', 'Running');
SET IDENTITY_INSERT [Shopping].[Category] OFF

SET IDENTITY_INSERT [Shopping].[Item] ON
INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [ShortDescription], [Description], [Price], [CategoryID]) VALUES (1, 'ITM001', 'Coleman 3 Person Instant Dome Tent', 'Dimensions - 213cm(L) x 213cm(W) x 168cm(H)', '<div id="prod-desc" class="description datablock">
                <p>The All new signature series from Coleman offers the simplicity of a traditional dome combined with the ease of their newly developed instant pitch technology. Arrive at your next camping trip and have your tent pitched in 60 seconds. This makes this tent the perfect companion for those quick weekend or overnight getaways and also if you''re planning to pitch your tent in the dark.</p>
<p><strong>Features:</strong></p>
<ul><li>Poles: Steel and fiberglass</li>
<li>Groundsheet: Polyethylene 1000D</li>
<li>Flysheet: 75D Polyester Taffeta (built-in)</li>
<li>Inner Sheet: 75D Polyester Taffeta</li>
<li>Water Resistance: WeatherTec System Keeps you dry Guaranteed</li>
<li>Built in rainfly</li>
<li>Maximum ventilation, four auto-roll windows</li>
<li>Pitches in less than 60 seconds</li>
</ul><p><strong>Specification</strong></p>
<ul><li>Dimensions: &nbsp;213cm X 213cm</li>
<li>Height: &nbsp;cm</li>
<li>Weight: kg</li>
<li>Sleeps: Up to 3 People</li>
<li>1 Year Warranty&nbsp;</li>
</ul>                </div>', 1249.00, 9);
INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [ShortDescription], [Description], [Price], [CategoryID]) VALUES (2, 'ITM002', 'Coleman Eight Person Evanston Tent', 'Dimensions - 3.66m(L) x 3.66m(W) x 1.93m(H)', '<div id="prod-desc" class="description datablock">
                <p>Make camping easier, and still have the room you need in a Coleman Evanston 8 Tent. It''s designed for simple setup and easy carrying?perfect for car campers and long trips. A door awning offers added protection from the storms and sun. Insta-Clip? Pole Attachments stand up to high wind and the WeatherTec? System?s patented welded floors and inverted protected seams help ensure you stay dry. The snag-free, continuous pole sleeves mean you only have to feed the poles once?reducing setup time to just 15 minutes. Inside, the 3.66 m x 3.66 m floor is large enough to fit two queen size airbeds. Before you lie down, attach the fly for protection from the rain, or on dry nights, gaze at the stars through the mesh roof.</p>
<p><strong>Features:</strong></p>
<ul><li>Exclusive WeatherTec System Keeps you dry Guaranteed</li>
<li>Electrical port to bring power and technology inside the tent</li>
<li>Rainfly with windows awnings, protection from rain and sun</li>
<li>WeatherTec System, patented welded floors and inverted seams keep water out</li>
<li>Rainfly for weather protection, mesh roof for more sunlight</li>
<li>Insta-Clip, Pole Attachments stand up to wind</li>
<li>Snag-free, continuous pole sleeves for easy, 15-minute setup</li>
<li>Made in China</li>
</ul><p><strong>Specification</strong></p>
<ul><li>Dimensions: &nbsp;3.66 m X 3.66 m footprint</li>
<li>Height: &nbsp;193 cm</li>
<li>Weight: kg</li>
<li>Sleeps: Up to 8 People</li>
<li>1 Year Warranty&nbsp;</li>
</ul>                </div>', 2999.00, 9);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (3, 'ITM003', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (4, 'ITM004', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (5, 'ITM005', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (6, 'ITM006', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (7, 'ITM007', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (8, 'ITM008', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (9, 'ITM009', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (10, 'ITM010', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (11, 'ITM011', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (12, 'ITM012', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (13, 'ITM013', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (14, 'ITM014', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (15, 'ITM015', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (16, 'ITM016', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (17, 'ITM017', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (18, 'ITM018', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (19, 'ITM019', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (20, 'ITM020', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (21, 'ITM021', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (22, 'ITM022', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (23, 'ITM023', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (24, 'ITM024', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (25, 'ITM025', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (26, 'ITM026', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (27, 'ITM027', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (28, 'ITM028', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (29, 'ITM029', '', '', 100.00, 1);
--INSERT INTO [Shopping].[Item] ([ItemID], [Code], [ItemName], [Description], [Price], [CategoryID]) VALUES (30, 'ITM030', '', '', 100.00, 1);
SET IDENTITY_INSERT [Shopping].[Item] OFF

--SET IDENTITY_INSERT [Shopping].[ItemPicture] ON
--INSERT INTO [Shopping].[ItemPicture] ([ItemPictureID], [ItemID], [Picture]) SELECT 1, 1, BulkColumn FROM Openrowset( Bulk '..\..\..\QCTestFE\Content\images\tent1.jpg', Single_Blob) as ItemPicture;
--INSERT INTO [Shopping].[ItemPicture] ([ItemPictureID], [ItemID], [Picture]) SELECT 2, 2, BulkColumn FROM Openrowset( Bulk '..\..\..\QCTestFE\Content\images\tent2.jpg', Single_Blob) as ItemPicture;
--SET IDENTITY_INSERT [Shopping].[ItemPicture] OFF