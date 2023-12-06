from PIL import Image

data = []

file = open("world_test.txt")
lines = file.readlines()
counter = 0

for line in lines:
    line_data = line.split(',')
    data.append(line_data)

width = len(data[0])
height = len(data)

img = Image.new( 'RGB', (width, height), "black") # Create a new black image
print(img.size[0])
print(img.size[1])
pixels = img.load() # Create the pixel map
for i in range(width):    # For every pixel:
    for j in range(height):
        print(str(i)+":"+str(j))
        val = int(float(data[i][j]) * 255)
        pixels[i,j] = (val, val, val) # Set the colour accordingly
        
img.save("./img.png")
input("press ENTER to exit")