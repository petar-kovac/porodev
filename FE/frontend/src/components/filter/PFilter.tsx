import { AppstoreOutlined, BarsOutlined } from '@ant-design/icons';
import { TrendingUpDimensions } from '@styled-icons/ionicons-outline/TrendingUp';
import { DatePicker, Select, Slider, Input } from 'antd';
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
          <RangePicker />
        </StyledFilesDateFilter>
      )}

      {propsObj.showFilterBySize && (
        <StyledFilesSlider>
          <StyledInput placeholder="Search folders &amp; files" />
        </StyledFilesSlider>
      )}

      <StyledFilesSelect>
        {propsObj.showSortByType && (
          <PSelect
            defaultValue="File extension"
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
            {/* {isList ? (
              <StyledAppstoreOutlined
                onClick={(e: any) => {
                  e.stopPropagation();
                  setIsList((prevState) => !prevState);
                }}
              />
            ) : (
              <StyledBarsOutlined
                onClick={(e: any) => {
                  e.stopPropagation();
                  setIsList((prevState) => !prevState);
                }}
              />
            )} */}
            <>
              <StyledAppstoreOutlined
                isList={isList}
                onClick={(e: any) => {
                  e.stopPropagation();
                  setIsList(true);
                }}
              />

              <StyledBarsOutlined
                isList={isList}
                onClick={(e: any) => {
                  e.stopPropagation();
                  setIsList(false);
                }}
              />
            </>
          </StyledToggleButton>
        )}
      </StyledFilesSelect>
    </StyledFilesHeader>
  );
};

const StyledFilesHeader = styled.div`
  display: flex;
  flex-wrap: wrap;
`;

const StyledToggleButton = styled.div`
  margin-left: auto;
`;
const StyledFilesSlider = styled.div`
  /* width: 32rem; */
  .ant-slider {
  }
`;

const StyledFilesDateFilter = styled.div`
  .ant-picker {
    border-radius: 1rem;
  }
`;

const StyledFilesSelect = styled.div`
  /* width: 32rem; */
  align-self: flex-end;
  display: flex;
  gap: 1.5rem;
  align-items: center;

  .ant-select-selector {
    border-radius: 1rem !important;
    color: #999;
  }
`;

const StyledInput = styled(Input)`
  border-radius: 1rem;
`;

export default PFilter;
