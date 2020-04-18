import matplotlib.pyplot as plt
import matplotlib
import numpy as np
import io

matplotlib.pyplot.ioff()
print(f"is_interactive = {matplotlib.is_interactive()}")

x = np.linspace(0, 2, 100)


fig, (ax1, ax2) = plt.subplots(nrows=1, ncols=2, figsize=(20,10))
ax1.plot(x, x, label='linear')  
ax1.plot(x, x**2, label='quadratic')  
ax1.set_xlabel('x label')  
ax1.set_ylabel('y label')  
ax1.set_title("First Plot")  
ax1.legend(loc='lower right')

ax2.plot(x, x**2, label='quadratic')  
ax2.plot(x, x**3, label='cubic') 
ax2.set_xlabel('x label')  
ax2.set_ylabel('y label')  
ax2.set_title("Second Plot")  
ax2.legend(loc='lower right')
fig.savefig("linear-quedratic.png")
fig.savefig("linear-quedratic.png")
buffer = io.BytesIO()
fig.savefig(buffer, format='png')
buffer.seek(0)
print(buffer.read())

plt.show()
input("Press Enter to continue...")
print("Done")

