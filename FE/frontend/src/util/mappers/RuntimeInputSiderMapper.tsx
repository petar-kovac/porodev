import { CloseOutlined } from '@ant-design/icons';
import { Input } from 'antd';
import { useSiderContext } from 'context/SiderContext';
import { FC } from 'react';
import styled from 'styled-components';

/**
 * Component to set input fields in a Runtime sider.
 */
const RuntimeSiderMapper: FC = () => {
  const { inputParameters, dispatchInput } = useSiderContext();

  return (
    <>
      {inputParameters.map((value: string, index: number) => {
        return (
          <StyledInputRow>
            <StyledInput
              value={value as unknown as string}
              placeholder="Add parameter"
              onChange={(e) => {
                dispatchInput({
                  type: 'CHANGE_INPUT_FIELD_VALUE',
                  payload: { e, index },
                });
              }}
            />
            <StyledClose
              onClick={() => {
                dispatchInput({
                  type: 'DELETE_SINGLE_FIELD',
                  payload: { index },
                });
              }}
            />
          </StyledInputRow>
        );
      })}
    </>
  );
};

const StyledInputRow = styled.div`
  display: flex;
  position: relative;
  align-items: center;
  width: 260px;
`;
const StyledInput = styled(Input)`
  border-radius: 12px;
  width: 260px;
`;
const StyledClose = styled(CloseOutlined)`
  position: absolute;
  color: red;
  cursor: pointer;
  left: 235px;
`;

export default RuntimeSiderMapper;
