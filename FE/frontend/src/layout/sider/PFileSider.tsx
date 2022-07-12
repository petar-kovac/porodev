import { Layout } from 'antd';
import {
  Dispatch,
  FC,
  MouseEventHandler,
  ReactNode,
  SetStateAction,
  useEffect,
} from 'react';
import styled from 'styled-components';

import { PlusCircleOutlined } from '@ant-design/icons';
import PButton from 'components/buttons/PButton';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import { StyledClose } from 'styles/icons/styled-icons';
import StyledIcon from 'styles/icons/StyledIcons';
import { IFilesCard } from 'types/card-data';
import { startRuntimeService } from 'service/runtime/runtime';
import { GetRuntimeModalData } from 'util/util-components/GetRuntimeModalData';
import RuntimeInputSiderMapper from 'util/mappers/RuntimeInputSiderMapper';
import RuntimeImageSiderMapper from 'util/mappers/RuntimeImageSiderMapper';
import { CreateImageAsparameter } from 'util/util-components/CreateImageAsParameter';
import { inputParamsChecker } from 'util/helpers/input-params-checker';
import SiderDataMapper from '../../util/mappers/SiderDataMapper';

const { Sider } = Layout;

interface IPFileSiderProps {
  cardData?: IFilesCard | null;
  data?: IFilesCard[] | null;
  type: 'folder' | 'file' | 'runtime';
  selectedCardId?: number | null;
  setCardData?: Dispatch<SetStateAction<IFilesCard | null>>;
  setData?: Dispatch<SetStateAction<IFilesCard[] | null>>;
  setSelectedCardId?: Dispatch<SetStateAction<number | null>>;
  onButtonClick?: MouseEventHandler<HTMLButtonElement>;
}

const PFileSider: FC<IPFileSiderProps> = ({
  cardData,
  data,
  setSelectedCardId = () => undefined,
  selectedCardId,
  type,
}) => {
  const {
    isLoading,
    isSiderVisible,
    projectId,
    setIsLoading,
    setIsSiderVisible,
    setIsModalVisible,
    setModalContent,
  } = usePageContext();

  const {
    inputParameters,
    setInputParameters,
    imageParameters,
    setImageParameters,
  } = useSiderContext();

  const handleClose = () => {
    setIsSiderVisible(false);
    // setCardData(null);
    setSelectedCardId(null);
  };

  const onStartRuntime = async () => {
    setIsLoading(true);

    // function to check if arguments are empty strings
    const checkedInputParams = inputParamsChecker(inputParameters);

    try {
      const res = await startRuntimeService({
        projectId,
        arguments: [...imageParameters, ...checkedInputParams],
      });
      const modalDataToRender: ReactNode = GetRuntimeModalData(res);
      setModalContent(modalDataToRender);
      setIsSiderVisible(false);
      setIsModalVisible(true);
      setIsLoading(false);
    } catch (err) {
      setIsLoading(false);
    }
  };

  const onAddImage = () => {
    if (data) {
      const modalDataToRender: ReactNode = CreateImageAsparameter(
        data,
        imageParameters,
        setImageParameters,
      );
      setModalContent(modalDataToRender);
      setIsModalVisible(true);
    }
  };

  useEffect(() => {
    setInputParameters(['']);
    setImageParameters([]);
  }, [selectedCardId]);

  return (
    <StyledFileSider
      collapsedWidth={0}
      collapsed={!isSiderVisible}
      width={320}
      onClick={(e) => e.stopPropagation()}
    >
      <StyledColumn>
        {cardData ? (
          <>
            <StyledRow>
              <StyledIcon type={type} />
            </StyledRow>
            <StyledTitle>{cardData?.title}</StyledTitle>
            <StyledContent>
              {cardData && <SiderDataMapper data={cardData} />}
            </StyledContent>
            {type === 'runtime' && (
              <>
                <StyledRow>
                  <StyledTitle>Add parameters</StyledTitle>
                  <PlusCircleOutlined
                    onClick={() => setInputParameters([...inputParameters, ''])}
                  />
                </StyledRow>
                <RuntimeInputSiderMapper />
                <StyledRow>
                  <StyledTitle>Add image</StyledTitle>
                  <PlusCircleOutlined onClick={onAddImage} />
                </StyledRow>
                <div style={{ display: 'flex', gap: 10 }}>
                  <RuntimeImageSiderMapper />
                </div>
              </>
            )}
            <PButton
              text={type === 'runtime' ? 'Start execution' : `Show ${type}`}
              onClick={onStartRuntime}
              isLoading={isLoading}
            />
          </>
        ) : (
          'Please select folder'
        )}
      </StyledColumn>
    </StyledFileSider>
  );
};

const StyledContent = styled.div`
  display: flex;
  flex-direction: column;
  gap: 10px;
  word-wrap: break-word;
`;
const StyledRow = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  justify-content: space-between;
  align-items: center;
`;
const StyledTitle = styled.div`
  font-size: 24px;
`;

const StyledColumn = styled.div`
  display: flex;
  flex-direction: column;
  align-items: space-around;
  width: 290px;
  flex: 1;
  gap: 15px;
`;

const StyledFileSider = styled(Sider).attrs({
  'data-testid': 'file-sider',
})`
  // change parent component if you know what are you doing, usually change .children class
  background-color: #fff;
  overflow-x: hidden;
  display: flex;
  width: 400px;

  flex-direction: column;
  box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);

  .ant-layout-sider-trigger {
    box-shadow: 1px 0px 6px rgba(34, 25, 25, 0.1);
  }

  .ant-layout-sider-children {
    display: flex;
    flex-direction: column;
    width: 400px;

    position: fixed;
    padding: 20px;
  }

  & li span {
    font-size: 1.9rem;
  }

  & li span svg {
    font-size: 1.9rem;
  }

  .ant-layout-sider-trigger {
    background-color: ${({ theme: { colors } }) => colors.primary};
  }
`;

export default PFileSider;
