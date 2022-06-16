import { DefaultOptionType } from 'antd/lib/select';
import { FC } from 'react';
import { Select } from 'antd';

interface IPSelectProps {
  defaultValue?: string;
  onChange:
    | ((value: string, option: DefaultOptionType | DefaultOptionType[]) => void)
    | undefined;
  data: object[]; // kako da definisem inteface objecta koji prima ovaj data
}
interface IDataObject {
  // kako da ovaj inteface ubacim u value: any
  key: string;
  value: string;
  children: string;
}
const { Option } = Select;

const PSelect: FC<IPSelectProps> = ({ defaultValue, onChange, data }) => {
  return (
    <Select defaultValue={defaultValue} onChange={onChange}>
      {data.map((value: any) => (
        <Option key={value.key} value={value.value}>
          {value.children}
        </Option>
      ))}
    </Select>
  );
};

export default PSelect;
