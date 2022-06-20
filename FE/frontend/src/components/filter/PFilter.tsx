import { AppstoreOutlined, BarsOutlined } from '@ant-design/icons';
import { DatePicker, Select, Slider } from 'antd';
import { DefaultOptionType } from 'antd/lib/select';
import PSelect from 'components/select/PSelect';
import { Dispatch, FC, SetStateAction } from 'react';
import styled from 'styled-components';
import {
  StyledAppstoreOutlined,
  StyledBarsOutlined,
} from 'styles/icons/styled-icons';

const { RangePicker } = DatePicker;

interface IPFilterProps {
  isList: boolean;
  setIsList: Dispatch<SetStateAction<boolean>>;
  activeFilters?: IACtiveFilters;
}
interface IACtiveFilters {
  showSortByTime?: boolean;
  showSortByType?: boolean;
  showFilterBySize?: boolean;
  showFilterByDate?: boolean;
  showToggleButton?: boolean;
}

const PFilter: FC<IPFilterProps> = ({
  isList,
  setIsList,
  activeFilters = {},
}) => {
  const propsObj = { ...activeFilters };

  // mocked select data until backed is ready
  const data = [
    { value: '1', key: '1', children: '.jpg' },
    { value: '2', key: '2', children: '.png' },
    { value: '3', key: '3', children: '.txt' },
    { value: '4', key: '4', children: '.cscs' },
  ];

  const data2 = [
    { value: '1', key: '1', children: 'Ascending' },
    { value: '2', key: '2', children: 'Descending' },
    { value: '3', key: '3', children: 'Newest' },
    { value: '4', key: '4', children: 'Oldest' },
  ];

  const handleChange = (
    value: string,
    option: DefaultOptionType | DefaultOptionType[],
  ) => {
    console.log(`selected ${value}`);
    console.log('options ', option);
  };

  return (
    <StyledFilesHeader>
      {propsObj.showFilterByDate && (
        <StyledFilesDateFilter>
          <h4>Filter files by date:</h4>
          <RangePicker />
        </StyledFilesDateFilter>
      )}

      {propsObj.showFilterBySize && (
        <StyledFilesSlider>
          <h4>Filter by size:</h4>
          <Slider range defaultValue={[0, 30]} />
        </StyledFilesSlider>
      )}

      <StyledFilesSelect>
        {propsObj.showSortByType && (
          <PSelect
            defaultValue="File type"
            onChange={handleChange}
            data={data}
          />
        )}

        {propsObj.showSortByTime && (
          <PSelect
            defaultValue="File type"
            onChange={handleChange}
            data={data2}
          />
        )}

        {propsObj.showToggleButton && (
          <StyledToggleButton>
            {isList ? (
              <StyledAppstoreOutlined onClick={() => setIsList(!isList)} />
            ) : (
              <StyledBarsOutlined onClick={() => setIsList(!isList)} />
            )}
          </StyledToggleButton>
        )}
      </StyledFilesSelect>
    </StyledFilesHeader>
  );
};

const StyledFilesHeader = styled.div`
  display: flex;
  justify-content: space-around;
  flex-wrap: wrap;
  width: 100%;
`;

const StyledToggleButton = styled.div`
  margin-left: auto;
`;
const StyledFilesSlider = styled.div`
  width: 32rem;
  .ant-slider {
    margin: 2.2rem 0.6rem 1rem;
    padding: 0 !important;
  }
`;

const StyledFilesDateFilter = styled.div`
  width: 32rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  .ant-picker {
    border-radius: 1rem;
  }
`;

const StyledFilesSelect = styled.div`
  width: 32rem;
  align-self: flex-end;
  display: flex;
  gap: 1rem;
  align-items: center;

  .ant-select-selector {
    border-radius: 10px !important;
    color: #999;
  }
`;

export default PFilter;
