import matplotlib.pyplot as plt
import numpy as np
import matplotlib as mpl

# disable matplotlib toolbar
mpl.rcParams['toolbar'] = 'None'


xpoints = ['01/07/2021', '04/07/2021', '11/07/2021', '18/07/2021', '25/07/2021', '01/08/2021', '08/08/2021', '15/08/2021', '22/08/2021', '29/08/2021',
           '05/09/2021', '12/09/2021', '19/09/2021', '26/09/2021', '03/10/2021', '10/10/2021', '17/10/2021', '24/10/2021', '31/10/2021', '07/11/2021']
ypoints = [-32.068332, -32.124332, -19.538332, -23.781784, -23.991733, -25.593401, -13.435547, -14.221397, -16.388681, 
          -3.872434, -1.447454, 7.482553, 6.850829, 5.072168, 1.958820, 16.740812, 15.898284, 6.069874, 10.087989, 9.295687]

fig = plt.figure('Thống kê tài sản')
ax = plt.subplot()

bars = plt.bar(xpoints, ypoints)


annot = ax.annotate("", xy=(0, 0), xytext=(-20, 20), textcoords="offset points",
                    bbox=dict(boxstyle="round", fc="w"),
                    arrowprops=dict(arrowstyle="->"))
annot.set_visible(False)


def update_annot(bar):

    x = bar.get_x() + bar.get_width()/2.
    y = bar.get_y() + bar.get_height()

    # print(ax.format_xdata(x))

    ax.format_ydata = lambda d: y
    # print(ax.format_ydata(y))

    # thay đổi định dạng format_coord thanh taskbar figure
    ax.format_coord = lambda x, y: 'Ngày: {}\n Tiền: {}'.format(ax.format_xdata(x), ax.format_ydata(y))

    annot.xy = (x, y)
    # text = "{:.3f}".format(y)
    text = 'Ngày: {}\n Tiền={}'.format(ax.format_xdata(x), ax.format_ydata(y))
    annot.set_text(text)
    annot.get_bbox_patch().set_alpha(0.4)


def hover(event):
    vis = annot.get_visible()
    if event.inaxes == ax:
        for bar in bars:
            cont, ind = bar.contains(event)
            if cont:
                update_annot(bar)
                annot.set_visible(True)
                fig.canvas.draw_idle()
                return
    if vis:
        annot.set_visible(False)
        fig.canvas.draw_idle()


fig.canvas.mpl_connect("motion_notify_event", hover)


# plt.axis('off')

plt.bar(xpoints, ypoints, color = 'pink', edgecolor='black')
# plt.legend(loc='upper right', bbox_to_anchor=(2, 2), frameon=True)

plt.xticks([])
# plt.yticks([])

plt.title('Tài sản')
plt.xlabel('Ngày')
plt.ylabel('Số tiền')
plt.show()
