import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { moduleMetadata, type Meta, type StoryObj } from '@storybook/angular';
import { ButtonComponent } from './button.component';

// More on how to set up stories at: https://storybook.js.org/docs/angular/writing-stories/introduction
const meta: Meta<ButtonComponent> = {
  title: 'OSOS/Shared/Components/Button',
  component: ButtonComponent,
  tags: ['autodocs'],
  render: (args: ButtonComponent) => ({
    props: {
      backgroundColor: null,
      ...args,
    },
  }),
  argTypes: {},
  decorators: [
    moduleMetadata({
      declarations: [],
      imports: [MatButtonModule, MatIconModule],
    }),
  ],
};

export default meta;
type Story = StoryObj<ButtonComponent>;

// More on writing stories with args: https://storybook.js.org/docs/angular/writing-stories/args
export const Add: Story = {
  args: {
    lableValue: 'Add User',
    Buttoncss: 'btn-save',
    iconValue: 'add',
    buttonType: 'button',
    Iconcss: 'material-icons',
  },
};

export const Cancel: Story = {
  args: {
    lableValue: 'Cancel',
    Buttoncss: 'btn-view',
    iconValue: 'cancel',
    buttonType: 'button',
    Iconcss: 'btn-view-icon',
  },
};

export const Delete: Story = {
  args: {
    lableValue: 'Delete',
    Buttoncss: 'btn-delete',
    iconValue: 'delete_outline',
    Iconcss: 'btn-save-icon material-icons-outlined',
  },
};
